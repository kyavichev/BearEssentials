namespace Bears.Core.MessengerInternal
{
    internal interface IFuncInvocation<out TReturn>
    {
        TReturn Invoke();
    }
    
    internal interface IFuncInvocation<in T0, out TReturn>
    {
        TReturn Invoke(T0 arg0);
    }
    
    internal interface IFuncInvocation<in T0, in T1, out TReturn>
    {
        TReturn Invoke(T0 arg0, T1 arg1);
    }
    
    internal interface IFuncInvocation<in T0, in T1, in T2, out TReturn>
    {
        TReturn Invoke(T0 arg0, T1 arg1, T2 arg2);
    }
}