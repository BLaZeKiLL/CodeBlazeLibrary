using System;
using System.ComponentModel;

using UnityEngine;

namespace CodeBlaze.Templates.TopDown.Controllers {

    [RequireComponent(typeof(CharacterController))]
    public class TopDownCharacterController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _gravity = -9.81f;
        
        [SerializeField] private float _groundDistance = 0.1f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;
        
        [SerializeField] private Camera _camera;
        
        [Description("Should the movment be according to global fixed axis or local relative axis")]
        [SerializeField] private bool _fixedAxisMovement;
        
        [Description("Distance from the player after which mouse rotation would be considered")]
        [Range(0.01f, 5f)]
        [SerializeField] private float _rotationSoftZone;

        public class MovementUpdateArgs : EventArgs {

            public Vector3 Direction { get; set; }
            public float Speed { get; set; }

        }

        public event EventHandler<MovementUpdateArgs> OnMovementUpdate;

        public bool CanMove { get; set; } = true;
        public bool IsGrounded { get; private set; }
        
        private CharacterController _characterController;
        private Vector3 _gvelocity;
        private Plane _ground;

        private void Start() {
            _characterController = GetComponent<CharacterController>();
            _ground = new Plane(Vector3.up, Vector3.zero);
        }

        private void Update() {
            IsGrounded = CheckIsGrounded();
            
            Gravity();
            Movement();
            Rotate();
        }

        private bool CheckIsGrounded() => Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        private void Gravity() {
            if (IsGrounded && _gvelocity.y < 0f) {
                _gvelocity.y = -1f;
            } else {
                _gvelocity.y += _gravity * Time.deltaTime;
            }
        }
        
        private void Movement() {
            if (!CanMove) return;
            
            var moveDirection = 
                (_fixedAxisMovement ? Vector3.right : transform.right) * Input.GetAxis("Horizontal") +
                (_fixedAxisMovement ? Vector3.forward : transform.forward) * Input.GetAxis("Vertical");

            if (moveDirection.magnitude >= 1f) moveDirection = moveDirection.normalized;
            
            OnMovementUpdate?.Invoke(this, new MovementUpdateArgs {
                Direction = moveDirection,
                Speed = _speed
            });
            
            _characterController.Move(moveDirection * (_speed * Time.deltaTime) + _gvelocity * Time.deltaTime);
            _ground.SetNormalAndPosition(Vector3.up, new Vector3(0f, transform.position.y, 0f));
        }

        private void Rotate() {
            if (Time.timeScale <= float.Epsilon) return; // paused
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!_ground.Raycast(ray, out float length)) return;

            var look = ray.GetPoint(length);
            
            if (Vector3.Distance(look, transform.position) <= _rotationSoftZone) return; // soft-zone check
            
            transform.LookAt(new Vector3(look.x, transform.position.y, look.z));
        }

    }

}