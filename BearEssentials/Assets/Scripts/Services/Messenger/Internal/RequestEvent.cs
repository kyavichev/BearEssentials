using System;

namespace Bears.Core.MessengerInternal
{
    internal class RequestEventBase<TFunc> : IMessageEvent<TFunc> where TFunc : Delegate
    {
        protected TFunc _func;
        
        public int Count => _func != null ? 1 : 0;
        
        public void AddListener(TFunc func) => _func = func;

        public void RemoveListener(TFunc func)
        {
            if (_func == func)
            {
                _func = null;
            }
        }
        
        public void RemoveListener(object action) => RemoveListener((TFunc)action);

    }
    
    internal class RequestEvent<TReturn> : RequestEventBase<Func<TReturn>>, IFuncInvocation<TReturn>
    {
        public TReturn Invoke()
        {
            return _func.Invoke();
        }
    }
    
    internal class RequestEvent<T, TReturn> : RequestEventBase<Func<T, TReturn>>, IFuncInvocation<T, TReturn>
    {
        public TReturn Invoke(T arg)
        {
            return _func.Invoke(arg);
        }
    }
    
    internal class RequestEvent<T0, T1, TReturn> : RequestEventBase<Func<T0, T1, TReturn>>, IFuncInvocation<T0, T1, TReturn>
    {
        public TReturn Invoke(T0 arg0, T1 arg1)
        {
            return _func.Invoke(arg0, arg1);
        }
    }
    
    internal class RequestEvent<T0, T1, T2, TReturn> : RequestEventBase<Func<T0, T1, T2, TReturn>>, IFuncInvocation<T0, T1, T2, TReturn>
    {
        public TReturn Invoke(T0 arg0, T1 arg1, T2 arg2)
        {
            return _func.Invoke(arg0, arg1, arg2);
        }
    }
}