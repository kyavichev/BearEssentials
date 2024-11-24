using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Bears.Core
{
    /// <summary>
    /// A poolable set. Discards to original pool when disposed.
    /// </summary>
    [PublicAPI]
    public class PooledSet<T> : HashSet<T>, IPoolable<PooledSet<T>>, IDisposable
    {
        IPool<PooledSet<T>> IPoolable<PooledSet<T>>.Pool { get; set; }

        void IPoolable<PooledSet<T>>.OnDraw()
        {
            
        }
        
        void IPoolable<PooledSet<T>>.OnDiscard()
        {
            Clear();
        }
        
        public void Dispose()
        {
            ((IPoolable<PooledSet<T>>) this).Pool?.Discard(this);
        }
    }
}