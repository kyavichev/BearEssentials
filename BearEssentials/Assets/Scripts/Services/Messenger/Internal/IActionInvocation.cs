namespace Bears.Core.MessengerInternal
{
    internal interface IActionInvocation
    {
        void Invoke();
    }
    
    internal interface IActionInvocation<in T>
    {
        void Invoke(T arg);
    }
    
    internal interface IActionInvocation<in T0, in T1>
    {
        void Invoke(T0 arg0, T1 arg1);
    }
    
    internal interface IActionInvocation<in T0, in T1, in T2>
    {
        void Invoke(T0 arg0, T1 arg1, T2 arg2);
    }
    
    internal interface IActionInvocation<in T0, in T1, in T2, in T3>
    {
        void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }
}