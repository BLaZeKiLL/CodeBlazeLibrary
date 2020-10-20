using UnityEngine;

namespace CodeBlaze.Library.Behaviour {

    [DefaultExecutionOrder(-100)]
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

        public static T Current { get; private set; }

        [SerializeField] private bool _isPersistant;

        protected virtual void Awake() {
            if (!Current) {
                Current = this as T;
                if (_isPersistant) DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

    }

}