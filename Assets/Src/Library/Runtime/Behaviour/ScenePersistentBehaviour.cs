using UnityEngine;

namespace CodeBlaze.GameFramework.Behaviour {

    public class ScenePersistentBehaviour : MonoBehaviour {

        private static ScenePersistentBehaviour instance;
        
        private void Awake() {
            if (instance != null) Destroy(gameObject);
            else {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

    }

}