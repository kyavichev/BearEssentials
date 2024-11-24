using System;
using JetBrains.Annotations;

namespace Bears.Core
{
    /// <summary>
    /// An interface for poolable objects
    /// </summary>
    /// <typeparam name="T">A class that implements the IPoolable interface</typeparam>
    [PublicAPI]
    public interface IPoolable<T> where T : class, IPoolable<T>
    {
        IPool<T> Pool { get; set; }

        void OnDraw();

        void OnDiscard();
    }
}