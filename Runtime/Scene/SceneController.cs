using System;
using System.Collections;

using UnityEngine;

namespace CodeBlaze.GameFramework.Scene {

    [DefaultExecutionOrder(-1200)]
    public abstract class SceneController : MonoBehaviour {

        public static SceneController Current { get; private set; }

        public class SceneEventArgs : EventArgs { }

        public event EventHandler<SceneEventArgs> OnSceneInitialized;

        public event EventHandler<SceneEventArgs> OnSceneDeInitialized; 

        protected abstract IEnumerator Bootstrap();
        
        private void Awake() {
            Current = this;
        }

        private IEnumerator Start() {
            yield return Bootstrap();

            OnInitialized();
        }
        
        protected virtual void OnInitialized() {
            OnSceneInitialized?.Invoke(this, new SceneEventArgs());
        }

        private void OnDisable() {
            OnSceneDeInitialized?.Invoke(this, new SceneEventArgs());
        }

    }

}
