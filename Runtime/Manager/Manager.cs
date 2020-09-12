using System;
using System.Collections;

using CodeBlaze.GameFramework.Behaviour;

using UnityEngine;

namespace CodeBlaze.GameFramework.Manager {

    public class Manager<T> where T : Manager<T>, new() {

        private InternalBehaviour Behaviour { get; }
        
        public static T Current { get; private set; }

        protected Manager() {
            Behaviour = InternalBehaviour.Create(typeof(T).Name);
            Behaviour.OnUpdateAction = OnUpdate;
            Behaviour.OnLateUpdateAction = OnLateUpdate;
            Behaviour.OnDrawGizmosAction = OnDrawGizmos;
            Behaviour.OnDestroyAction = () => {
                OnDestroy();
                Current = default;
            };
            OnInitialize(); // Could lead to problems
        }

        public static void Initialize() {
            if (Current != null)
                throw new InvalidOperationException("Instance of this manager is already presented in the scene.");
            Current = new T();
        }

        protected virtual void OnInitialize() {}
        
        protected virtual void OnDestroy() {}
        
        protected virtual void OnUpdate() {}
        
        protected virtual void OnLateUpdate() {}
        
        protected virtual void OnDrawGizmos() {}

        protected TChild InstantiateChild<TChild>(TChild original) where TChild : UnityEngine.Object =>
            UnityEngine.Object.Instantiate<TChild>(original, Behaviour.transform);

        protected Coroutine StartCoroutine(IEnumerator routine) => Behaviour.StartCoroutine(routine);

        protected void StopCoroutine(IEnumerator routine) => Behaviour.StopCoroutine(routine);

        protected void StopCoroutine(Coroutine routine) => Behaviour.StopCoroutine(routine);

    }

}