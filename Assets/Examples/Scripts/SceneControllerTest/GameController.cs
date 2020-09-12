using System.Collections;

using CodeBlaze.GameFramework.Manager;
using CodeBlaze.GameFramework.Manager.TickManager;
using CodeBlaze.GameFramework.Scene;

using UnityEngine;

namespace SceneControllerTest {

    public class GameController : SceneController {
        
        protected override IEnumerator Bootstrap() {
            Debug.Log("Init Phase");

            yield return null;
            
            Manager<TickManager>.Initialize();

            yield return null;
            
            Debug.Log("Final Phase");
            
            yield return  null;
        }

        protected override void OnInitialized() {
            Debug.Log("On Initialized");
            base.OnInitialized(); // Signals game behaviours on init            
        }

    }

}