using JetBrains.Annotations;

namespace Bears.Core
{
    /// <summary>
    /// An interface for pools
    /// </summary>
    [PublicAPI]
    public interface IPool<T> where T : class, IPoolable<T>
    {
        T Draw();
        
        void Discard(T item);
    }
}