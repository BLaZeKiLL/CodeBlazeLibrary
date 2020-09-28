namespace CodeBlaze.Library.Collections.Pools {

    public interface IObjectPool<T> {

        int Size { get; }
        
        T Claim();

        void Reclaim(T instance);

    }
    
    public delegate T builder<T>(int index);

}