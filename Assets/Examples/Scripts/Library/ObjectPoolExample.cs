using CodeBlaze.Library.Collections.Pools;

using UnityEngine;

namespace CodeBlaze.Examples.Library {

    public class ObjectPoolExample : MonoBehaviour {

        private LazyObjectPool<string> _pool;
        
        private void Start() {
            _pool = new LazyObjectPool<string>(5, index => $"Hello : {index}");
            
            Debug.Log(_pool.Claim());
        }

    }

}