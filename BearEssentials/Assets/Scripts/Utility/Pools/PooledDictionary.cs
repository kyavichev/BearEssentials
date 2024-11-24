using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Bears.Core
{
    /// <summary>
    /// A poolable dictionary. Discards to original pool when disposed.
    /// </summary>
    [PublicAPI]
    public class PooledDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IPoolable<PooledDictionary<TKey, TValue>>, IDisposable
    {
        IPool<PooledDictionary<TKey, TValue>> IPoolable<PooledDictionary<TKey, TValue>>.Pool { get; set; }
        
        void IPoolable<PooledDictionary<TKey, TValue>>.OnDraw()
        {
            
        }
        
        void IPoolable<PooledDictionary<TKey, TValue>>.OnDiscard()
        {
            Clear();
        }
        
        public void Dispose()
        {
            ((IPoolable<PooledDictionary<TKey, TValue>>)this).Pool.Discard(this);
        }
    }
}