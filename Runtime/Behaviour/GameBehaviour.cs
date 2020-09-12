using System;

using CodeBlaze.GameFramework.Scene;

using UnityEngine;

namespace CodeBlaze.GameFramework.Behaviour {

    public class GameBehaviour : MonoBehaviour {

        private void Awake() {
            try {
                SceneController.Current.OnSceneInitialized += OnSceneInitialized;
                SceneController.Current.OnSceneDeInitialized += OnSceneDeInitialized;
            } catch (NullReferenceException) {
                throw new Exception("Scene Controller not present in the scene");
            }
        }

        protected virtual void OnSceneDeInitialized(object sender, SceneController.SceneEventArgs e) {
            SceneController.Current.OnSceneInitialized -= OnSceneDeInitialized;
        }

        protected virtual void OnSceneInitialized(object sender, SceneController.SceneEventArgs e) {
            SceneController.Current.OnSceneInitialized -= OnSceneInitialized;
        }

    }

}