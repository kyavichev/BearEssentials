using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Bears.Core
{
    /// <summary>
    /// A poolable list. Discards to original pool when disposed.
    /// </summary>
    [PublicAPI]
    public class PooledList<T> : List<T>, IPoolable<PooledList<T>>, IDisposable
    {
        IPool<PooledList<T>> IPoolable<PooledList<T>>.Pool { get; set; }

        void IPoolable<PooledList<T>>.OnDraw()
        {
            
        }

        void IPoolable<PooledList<T>>.OnDiscard()
        {
            Clear();
        }
        
        public void Dispose()
        {
            ((IPoolable<PooledList<T>>)this).Pool?.Discard(this);
        }
    }
}