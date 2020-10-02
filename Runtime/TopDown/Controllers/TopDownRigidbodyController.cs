using System;
using System.ComponentModel;

using UnityEngine;

namespace CodeBlaze.Templates.TopDown.Controllers {

    [RequireComponent(typeof(Rigidbody))]
    public class TopDownRigidbodyController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;
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

        public event EventHandler<MovementUpdateArgs> OnMovementInputUpdate;
        public event EventHandler<MovementUpdateArgs> OnMovementFixedUpdate;

        public bool CanMove { get; set; } = true;
        public bool IsGrounded { get; private set; }

        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;
        private Plane _ground;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            _ground = new Plane(Vector3.up, Vector3.zero);
        }
        
        private void Update() {
            IsGrounded = CheckIsGrounded();
            GetInput();
        }

        private void FixedUpdate() {
            Move();
            Rotate();
        }
        
        private bool CheckIsGrounded() => Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        private void GetInput() {
            if (!CanMove) return;
            
            _moveDirection = 
                (_fixedAxisMovement ? Vector3.right : transform.right) * Input.GetAxis("Horizontal") +
                (_fixedAxisMovement ? Vector3.forward : transform.forward) * Input.GetAxis("Vertical");
            
            OnMovementInputUpdate?.Invoke(this, new MovementUpdateArgs {
                Direction = _moveDirection,
                Speed = _speed
            });
            
            if (_moveDirection.magnitude >= 1f) _moveDirection = _moveDirection.normalized;
        }

        private void Move() {
            if (!CanMove) return;
            
            OnMovementFixedUpdate?.Invoke(this, new MovementUpdateArgs {
                Direction = _moveDirection,
                Speed = _speed
            });
            
            _rigidbody.MovePosition(transform.position + (_moveDirection * _speed * Time.fixedDeltaTime));
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