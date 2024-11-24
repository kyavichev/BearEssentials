namespace Bears.Core
{
    public interface IMessenger : IMessageRegistrar, IListenerObjectDeregistration
    {
        void Send(in MsgId id);
        void Send<T>(in MsgId<T> id, T arg);
        void Send<T0, T1>(in MsgId<T0, T1> id, T0 arg0, T1 arg1);
        void Send<T0, T1, T2>(in MsgId<T0, T1, T2> id, T0 arg0, T1 arg1, T2 arg2);
        void Send<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
        
        TReturn Request<TReturn>(in MsgId<TReturn> id);
        TReturn Request<T, TReturn>(in MsgId<T, TReturn> id, T arg0);
        TReturn Request<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, T0 arg0, T1 arg1);
        TReturn Request<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, T0 arg0, T1 arg1, T2 arg2);

        bool HasListener(in MsgId id);
        bool HasListener<T>(in MsgId<T> id);
        bool HasListener<T0, T1>(in MsgId<T0, T1> id);
        bool HasListener<T0, T1, T2>(in MsgId<T0, T1, T2> id);
        bool HasListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id);
    }
}