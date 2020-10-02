using CodeBlaze.Examples.Manager;
using CodeBlaze.GameFramework.Behaviour;
using CodeBlaze.GameFramework.Manager.TickManager;

using UnityEngine;

namespace CodeBlaze.Examples {

    public class ExampleGameBehaviour : GameBehaviour {

        private void Start() {
            Debug.Log("Game Test Start");
        }

        protected override void OnSceneInitialized(object sender, GameFramework.Scene.SceneController.SceneEventArgs e) {
            Debug.Log("Tester Scene Initialized");
            Debug.Log("Wrapping Manager : " + WrappingManagerExample.Current.GetDemoName());
            TickManager.Current.OnTick += OnTick;
            base.OnSceneInitialized(sender, e); // removes subscription
        }

        protected override void OnSceneDeInitialized(object sender, GameFramework.Scene.SceneController.SceneEventArgs e) {
            // required until better DeInitialized solution is there
            if (TickManager.Current != null) TickManager.Current.OnTick -= OnTick;
            base.OnSceneDeInitialized(sender, e);
        }
        
        private void OnTick(object sender, TickManager.TickEventArgs e) {
            Debug.Log($"Tick : {e.Tick}");
        }

    }

}