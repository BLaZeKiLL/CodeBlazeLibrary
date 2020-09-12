using System.Collections.Generic;

namespace CodeBlaze.Library.Collections.Pools {

    public class ObjectPool<T> : IObjectPool<T> {

        private readonly Queue<T> Pool;

        public ObjectPool(IEnumerable<T> objects) {
            Pool = new Queue<T>(objects);
        }

        public int Size => Pool.Count;
        
        public T Claim() => Pool.Dequeue();

        public void Reclaim(T poolObject) => Pool.Enqueue(poolObject);

    }

}