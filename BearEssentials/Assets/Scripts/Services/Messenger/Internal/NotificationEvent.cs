using System;

namespace Bears.Core.MessengerInternal
{
    internal abstract class NotificationEventBase<TAction> : IMessageEvent<TAction> where TAction : Delegate
    {
        protected readonly Pool<PooledList<TAction>> _pool;
        protected readonly PooledList<TAction> _actions;
        
        public int Count => _actions.Count;

        internal NotificationEventBase(Pool<PooledList<TAction>> pool)
        {
            _pool = pool;
            _actions = pool.Draw();
        }

        ~NotificationEventBase()
        {
            _pool?.Discard(_actions);
        }
        
        public void AddListener(TAction action) => _actions.Add(action);

        public void RemoveListener(TAction action) => _actions.Remove(action);
        
        public void RemoveListener(object action) => _actions.Remove((TAction)action);
    }
    
    internal class NotificationEvent : NotificationEventBase<Action>, IActionInvocation
    {
        public NotificationEvent(Pool<PooledList<Action>> pool) : base(pool) { }

        public void Invoke()
        {
            if (_actions.Count == 0)
            {
                return;
            }

            using var actionsCopy = _pool.Draw();
            actionsCopy.AddRange(_actions);
            for (int i = 0, count = actionsCopy.Count; i < count; ++i)
            {
                actionsCopy[i].Invoke();
            }
        }
    }
    
    internal class NotificationEvent<T> : NotificationEventBase<Action<T>>, IActionInvocation<T>
    {
        public NotificationEvent(Pool<PooledList<Action<T>>> pool) : base(pool) { }

        public void Invoke(T arg)
        {
            if (_actions.Count == 0)
            {
                return;
            }

            using var actionsCopy = _pool.Draw();
            actionsCopy.AddRange(_actions);
            for (int i = 0, count = actionsCopy.Count; i < count; ++i)
            {
                actionsCopy[i].Invoke(arg);
            }
        }
    }
    
    internal class NotificationEvent<T0, T1> : NotificationEventBase<Action<T0, T1>>, IActionInvocation<T0, T1>
    {
        public NotificationEvent(Pool<PooledList<Action<T0, T1>>> pool) : base(pool) { }

        public void Invoke(T0 arg0, T1 arg1)
        {
            if (_actions.Count == 0)
            {
                return;
            }

            using var actionsCopy = _pool.Draw();
            actionsCopy.AddRange(_actions);
            for (int i = 0, count = actionsCopy.Count; i < count; ++i)
            {
                actionsCopy[i].Invoke(arg0, arg1);
            }
        }
    }
    
    internal class NotificationEvent<T0, T1, T2> : NotificationEventBase<Action<T0, T1, T2>>, IActionInvocation<T0, T1, T2>
    {
        public NotificationEvent(Pool<PooledList<Action<T0, T1, T2>>> pool) : base(pool) { }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2)
        {
            if (_actions.Count == 0)
            {
                return;
            }

            using var actionsCopy = _pool.Draw();
            actionsCopy.AddRange(_actions);
            for (int i = 0, count = actionsCopy.Count; i < count; ++i)
            {
                actionsCopy[i].Invoke(arg0, arg1, arg2);
            }
        }
    }
    
    internal class NotificationEvent<T0, T1, T2, T3> : NotificationEventBase<Action<T0, T1, T2, T3>>, IActionInvocation<T0, T1, T2, T3>
    {
        public NotificationEvent(Pool<PooledList<Action<T0, T1, T2, T3>>> pool) : base(pool) { }

        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_actions.Count == 0)
            {
                return;
            }

            using var actionsCopy = _pool.Draw();
            actionsCopy.AddRange(_actions);
            for (int i = 0, count = actionsCopy.Count; i < count; ++i)
            {
                actionsCopy[i].Invoke(arg0, arg1, arg2, arg3);
            }
        }
    }
}