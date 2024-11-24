using System;

namespace Bears.Core
{
    public interface IMessageRegistrar
    {
        void AddListener(in MsgId id, Action listener);
        void AddListener<T>(in MsgId<T> id, Action<T> listener);
        void AddListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener);
        void AddListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener);
        void AddListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener);
        
        void AddListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener);
        void AddListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener);
        void AddListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener);
        void AddListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener);
        
        void RemoveListener(in MsgId id, Action listener);
        void RemoveListener<T>(in MsgId<T> id, Action<T> listener);
        void RemoveListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener);
        void RemoveListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener);
        void RemoveListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener);
        
        void RemoveListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener);
        void RemoveListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener);
        void RemoveListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener);
        void RemoveListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener);
    }
}