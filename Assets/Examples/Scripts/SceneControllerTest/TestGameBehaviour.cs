using System;

using CodeBlaze.GameFramework.Behaviour;
using CodeBlaze.GameFramework.Manager.TickManager;
using CodeBlaze.GameFramework.Scene;

using UnityEngine;

namespace SceneControllerTest {

    public class TestGameBehaviour : GameBehaviour {

        private void Start() {
            Debug.Log("Game Test Start");
        }

        protected override void OnSceneInitialized(object sender, SceneController.SceneEventArgs e) {
            Debug.Log("Tester Scene Initialized");
            TickManager.Current.OnTick += OnTick;
            base.OnSceneInitialized(sender, e); // removes subcription
        }

        protected override void OnSceneDeInitialized(object sender, SceneController.SceneEventArgs e) {
            TickManager.Current.OnTick -= OnTick;
            base.OnSceneDeInitialized(sender, e);
        }
        
        private void OnTick(object sender, TickManager.TickEventArgs e) {
            Debug.Log($"Tick : {e.Tick}");
        }

    }

}