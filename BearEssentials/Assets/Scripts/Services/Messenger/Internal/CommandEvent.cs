using System;

namespace Bears.Core.MessengerInternal
{
    internal class CommandEventBase<TAction> : IMessageEvent<TAction> where TAction : Delegate
    {
        protected TAction _action;
        
        public int Count => _action != null ? 1 : 0;
        
        public void AddListener(TAction action) => _action = action;

        public void RemoveListener(TAction action)
        {
            if (_action == action)
            {
                _action = null;
            }
        }
        
        public void RemoveListener(object action) => RemoveListener((TAction)action);

    }
    
    internal class CommandEvent : CommandEventBase<Action>, IActionInvocation
    {
        public void Invoke()
        {
            _action.Invoke();
        }
    }
    
    internal class CommandEvent<T> : CommandEventBase<Action<T>>, IActionInvocation<T>
    {
        public void Invoke(T arg)
        {
            _action.Invoke(arg);
        }
    }
    
    internal class CommandEvent<T0, T1> : CommandEventBase<Action<T0, T1>>, IActionInvocation<T0,T1>
    {
        public void Invoke(T0 arg0, T1 arg1)
        {
            _action.Invoke(arg0, arg1);
        }
    }
    
    internal class CommandEvent<T0, T1, T2> : CommandEventBase<Action<T0, T1, T2>>, IActionInvocation<T0, T1, T2>
    {
        public void Invoke(T0 arg0, T1 arg1, T2 arg2)
        {
            _action.Invoke(arg0, arg1, arg2);
        }
    }
    
    internal class CommandEvent<T0, T1, T2, T3> : CommandEventBase<Action<T0, T1, T2, T3>>, IActionInvocation<T0, T1, T2, T3>
    {
        public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            _action.Invoke(arg0, arg1, arg2, arg3);
        }
    }
}