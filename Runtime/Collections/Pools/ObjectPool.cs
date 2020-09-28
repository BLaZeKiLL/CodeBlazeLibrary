using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Pools {

    public class ObjectPool<T> : IObjectPool<T> {

        private readonly Queue<T> _pool;

        public ObjectPool(IEnumerable<T> objects) {
            _pool = new Queue<T>(objects);
        }

        public ObjectPool(int size, builder<T> builder) {
            var temp = new T[size];

            for (int i = 0; i < size; i++) {
                temp[i] = builder(i);
            }
            
            _pool = new Queue<T>(temp);
        }

        public int Size => _pool.Count;
        
        public T Claim() => _pool.Dequeue();

        public void Reclaim(T instance) => _pool.Enqueue(instance);

    }

}