using System;
using JetBrains.Annotations;


namespace Bears.Core
{
    /// <summary>
    /// Generic pool class for pooling C# classes
    /// </summary>
    /// <typeparam name="T">A class that implements IPoolable and IDisposable</typeparam>
    [PublicAPI]
    public class Pool<T> : PoolBase<T> where T : class, IPoolable<T>, IDisposable, new()
    {
        /// <summary>
        /// Construct a pool with an initial capacity.
        /// </summary>
        /// <param name="initialSize">An initial capacity</param>
        public Pool(int initialSize) : base (initialSize, () => new T())
        {

        }
    }
}