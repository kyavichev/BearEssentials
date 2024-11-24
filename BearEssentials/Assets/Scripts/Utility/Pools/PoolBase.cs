using System;
using System.Collections.Generic;

namespace Bears.Core
{
    /// <summary>
    /// Base class for pools.
    /// </summary>
    /// <typeparam name="T">A class that implements IPoolable</typeparam>
    public class PoolBase<T> : IPool<T> where T : class, IPoolable<T>
    {
        private readonly object _lockObject = new Object();
        private readonly Stack<T> _pool;

        private readonly Func<T> _create;

        public int AvailableCount
        {
            get
            {
                lock (_lockObject)
                {
                    return _pool.Count;
                }
            }
        }

        /// <summary>
        /// Construct a pool with an initial capacity.
        /// </summary>
        /// <param name="initialSize">An initial capacity</param>
        /// <param name="create">A create function</param>
        public PoolBase(int initialSize, Func<T> create)
        {
            _pool = new Stack<T>(initialSize);

            _create = create ?? throw new ArgumentNullException(nameof(create));

            for (int i = 0; i < initialSize; ++i)
            {
                T item = _create();
                item.Pool = this;
                _pool.Push(item);
            }
        }

        public T Draw()
        {
            T item;
            lock (_lockObject)
            {
                if (_pool.Count > 0)
                {
                    item = _pool.Pop();
                }
                else
                {
                    item = _create();
                    item.Pool = this;
                }
            }
            
            item.OnDraw();
            return item;
        }

        public void Discard(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (item.Pool != this)
            {
                throw new ArgumentException("Item does not belong to pool");
            }
            
            lock (_lockObject)
            {
                if (ContainsUnused(item))
                {
                    throw new Exception("Discarded item already exists in pool.");
                }

                _pool.Push(item);
            }
            
            item.OnDiscard();
        }

        private bool ContainsUnused(T item)
        {
            lock (_lockObject)
            {
                foreach (T poolable in _pool)
                {
                    if (ReferenceEquals(item, poolable))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}