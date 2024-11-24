using System;
using System.Collections;

namespace Bears.Core
{
    /// <summary>
    /// A temporary listener handle that removes the listener action on disposal.
    /// </summary>
    public readonly struct TemporaryListener : IDisposable
    {
        private readonly IMessenger _messenger;
        private readonly int _idHash;
        private readonly object _action;

        private TemporaryListener(IMessenger messenger, int idHash, object action)
        {
            _messenger = messenger;
            _idHash = idHash;
            _action = action;
        }

        public void Dispose()
        {
            _messenger?.RemoveListener(_idHash, _action);
        }

        /// <summary>
        /// Creates a temporary disposable listener
        /// </summary>
        public static TemporaryListener Create(IMessenger messenger, MsgId id, Action listener)
        {
            messenger.AddListener(id, listener);
            return new TemporaryListener(messenger, id.hash, listener);
        }
        
        /// <summary>
        /// Creates a temporary disposable listener
        /// </summary>
        public static TemporaryListener Create<T>(IMessenger messenger, MsgId<T> id, Action<T> listener)
        {
            messenger.AddListener(id, listener);
            return new TemporaryListener(messenger, id.hash, listener);
        }
        
        /// <summary>
        /// Creates a temporary disposable listener
        /// </summary>
        public static TemporaryListener Create<T0, T1>(IMessenger messenger, MsgId<T0, T1> id, Action<T0, T1> listener)
        {
            messenger.AddListener(id, listener);
            return new TemporaryListener(messenger, id.hash, listener);
        }
        
        /// <summary>
        /// Creates a temporary disposable listener
        /// </summary>
        public static TemporaryListener Create<T0, T1, T2>(IMessenger messenger, MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener)
        {
            messenger.AddListener(id, listener);
            return new TemporaryListener(messenger, id.hash, listener);
        }
        
        /// <summary>
        /// Creates a temporary disposable listener
        /// </summary>
        public static TemporaryListener Create<T0, T1, T2, T3>(IMessenger messenger, MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener)
        {
            messenger.AddListener(id, listener);
            return new TemporaryListener(messenger, id.hash, listener);
        }
        
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator Wait(IMessenger messenger, MsgId id, Func<bool> predicate = null)
        {
            var done = false;
            using var _ = Create(messenger, id, () => done = predicate == null || predicate.Invoke());
            while (!done)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator Wait<T>(IMessenger messenger, MsgId<T> id, Func<T, bool> predicate = null)
        {
            var done = false;
            using var _ = Create(messenger, id, arg => done = predicate == null || predicate.Invoke(arg));
            while (!done)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator Wait<T0, T1>(IMessenger messenger, MsgId<T0, T1> id, Func<T0, T1, bool> predicate = null)
        {
            var done = false;
            using var _ = Create(messenger, id, (arg0, arg1) => done = predicate == null || predicate.Invoke(arg0, arg1));
            while (!done)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator Wait<T0, T1, T2>(IMessenger messenger, MsgId<T0, T1, T2> id, Func<T0, T1, T2, bool> predicate = null)
        {
            var done = false;
            using var _ = Create(messenger, id, (arg0, arg1, arg2) =>
                done = predicate == null || predicate.Invoke(arg0, arg1, arg2));
            while (!done)
            {
                yield return null;
            }
        }
        
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator Wait<T0, T1, T2, T3>(IMessenger messenger, MsgId<T0, T1, T2, T3> id, Func<T0, T1, T2, T3, bool> predicate = null)
        {
            var done = false;
            using var _ = Create(messenger, id, (arg0, arg1, arg2, arg3) =>
                done = predicate == null || predicate.Invoke(arg0, arg1, arg2, arg3));
            while (!done)
            {
                yield return null;
            }
        }
    }
}