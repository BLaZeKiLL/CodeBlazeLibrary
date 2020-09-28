using System;
using System.Collections;

using CodeBlaze.GameFramework.Behaviour;

using UnityEngine;

namespace CodeBlaze.GameFramework.Manager {

    public class WrappingLazyManager<T, V> where T : Manager<T>, new() where V : MonoBehaviour {

        private static Lazy<T> _current = new Lazy<T>(() => new T());
        
        private InternalBehaviour _Behaviour { get; }
        
        protected V Behaviour { get; }

        public static T Current {
            get => _current.Value;
        }
        
        protected WrappingLazyManager() {
            var gameObj = new GameObject(typeof(T).Name);
            Behaviour = gameObj.AddComponent<V>();
            _Behaviour = gameObj.AddComponent<InternalBehaviour>();
            _Behaviour.OnUpdateAction = OnUpdate;
            _Behaviour.OnLateUpdateAction = OnLateUpdate;
            _Behaviour.OnDrawGizmosAction = OnDrawGizmos;
            _Behaviour.OnDestroyAction = () => {
                OnDestroy();
                _current = default;
            };
            OnInitialize(); // Could lead to problems
        }
        
        protected virtual void OnInitialize() {}
        
        protected virtual void OnDestroy() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnLateUpdate() {}
        
        protected virtual void OnDrawGizmos() {}
        
        protected TChild InstantiateChild<TChild>(TChild original) where TChild : UnityEngine.Object =>
            UnityEngine.Object.Instantiate<TChild>(original, _Behaviour.transform);

        protected Coroutine StartCoroutine(IEnumerator routine) => _Behaviour.StartCoroutine(routine);

        protected void StopCoroutine(IEnumerator routine) => _Behaviour.StopCoroutine(routine);

        protected void StopCoroutine(Coroutine routine) => _Behaviour.StopCoroutine(routine);

    }

}