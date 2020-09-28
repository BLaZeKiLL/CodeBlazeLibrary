using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Pools {

    public class LazyObjectPool<T> : IObjectPool<T> {

        private builder<T> _builder;
        private readonly Queue<T> _pool;

        private int _instanceCount;
        
        public LazyObjectPool(int size, builder<T> builder) {
            _builder = builder;
            _pool = new Queue<T>(size);
        }
        
        public int Size => _pool.Count;
        public int Length => _instanceCount;

        public T Claim() {
            if (_pool.Count != 0) return _pool.Dequeue();

            _instanceCount++;

            return _builder(_instanceCount);

        }

        public void Reclaim(T instance) => _pool.Enqueue(instance);

    }

}