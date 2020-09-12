using System;

using UnityEngine;

namespace CodeBlaze.GameFramework.Behaviour {

    internal class InternalBehaviour : MonoBehaviour {

        public Action OnUpdateAction { get; set; }

        public Action OnLateUpdateAction { get; set; }

        public Action OnDestroyAction { get; set; }

        public Action OnDrawGizmosAction { get; set; }
        
        public static InternalBehaviour Create(string name) {
            return new GameObject(name).AddComponent<InternalBehaviour>();
        }

        private void Update() {
            OnUpdateAction?.Invoke();
        }

        private void LateUpdate() {
            OnLateUpdateAction?.Invoke();
        }

        private void OnDestroy() {
            OnDestroyAction?.Invoke();
        }

        private void OnDrawGizmos() {
            OnDrawGizmosAction?.Invoke();
        }

    }

}