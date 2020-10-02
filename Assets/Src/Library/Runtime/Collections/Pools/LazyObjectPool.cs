using System;
using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Pools {

    public class LazyObjectPool<T> : IObjectPool<T> {

        private readonly Queue<T> _pool;
        
        private readonly Func<int, T> _builder;
        private readonly Action<T> _onClaim;
        private readonly Action<T> _onReclaim;

        private int _instanceCount;
        private int _size;
        
        public LazyObjectPool(int size, Func<int, T> builder, Action<T> onClaim = null, Action<T> onReclaim = null) {
            _size = size;
            _pool = new Queue<T>(size);
            _builder = builder;
            _onClaim = onClaim;
            _onReclaim = onReclaim;
        }
        
        public int Size => _pool.Count;

        public T Claim() {
            T item;

            if (_pool.Count != 0) item = _pool.Dequeue();
            else if (_instanceCount < _size) {
                _instanceCount++;
                item = _builder(_instanceCount);
            } else {
                throw new IndexOutOfRangeException("Pool Size exceeded");
            }
            
            _onClaim?.Invoke(item);

            return item;
        }

        public void Reclaim(T item) {
            _onReclaim?.Invoke(item);
            _pool.Enqueue(item);
        }

    }

}