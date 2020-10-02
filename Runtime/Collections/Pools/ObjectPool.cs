using System;
using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Pools {

    public class ObjectPool<T> : IObjectPool<T> {

        private readonly Queue<T> _pool;

        private readonly Action<T> _onClaim;
        private readonly Action<T> _onReclaim;
        
        public ObjectPool(IEnumerable<T> objects, Action<T> onClaim = null, Action<T> onReclaim = null) {
            _pool = new Queue<T>(objects);
            _onClaim = onClaim;
            _onReclaim = onReclaim;
        }

        public ObjectPool(int size, Func<int, T> builder, Action<T> onClaim = null, Action<T> onReclaim = null) {
            var temp = new T[size];

            for (int i = 0; i < size; i++) {
                temp[i] = builder(i);
            }
            
            _pool = new Queue<T>(temp);
            _onClaim = onClaim;
            _onReclaim = onReclaim;
        }

        public int Size => _pool.Count;

        public T Claim() {
            var item = _pool.Dequeue();
            _onClaim?.Invoke(item);
            return item;
        }

        public void Reclaim(T item) {
            _onReclaim?.Invoke(item);
            _pool.Enqueue(item);
        }

    }

}