using System;
using System.Collections;

namespace Bears.Core
{
    public static class MessengerWaitExt
    {
        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator WaitFor(this IMessenger messenger, MsgId id, Func<bool> predicate = null) =>
            TemporaryListener.Wait(messenger, id, predicate);

        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator WaitFor<T>(this IMessenger messenger, MsgId<T> id, Func<T, bool> predicate = null) =>
            TemporaryListener.Wait(messenger, id, predicate); 

        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator WaitFor<T0, T1>(this IMessenger messenger, MsgId<T0, T1> id, Func<T0, T1, bool> predicate = null) =>
            TemporaryListener.Wait(messenger, id, predicate);

        /// <summary>
        /// Yields until message is received and predicate is null or returns true
        /// </summary>
        public static IEnumerator WaitFor<T0, T1, T2>(this IMessenger messenger, MsgId<T0, T1, T2> id, Func<T0, T1, T2, bool> predicate = null) =>
            TemporaryListener.Wait(messenger, id, predicate);
    }
}