using System;

namespace Bears.Core.MessengerInternal
{
    public class MessengerService : ServiceComponent<IMessengerService, MessengerService>, IMessengerService   
    {
        private readonly Messenger _messenger = new Messenger();
        
        protected override void OnRegister()
        {
            
        }
        
        protected override void OnDeregister()
        {
            
        }

        public void Send(in MsgId id) => _messenger.Send(id);
        public void Send<T>(in MsgId<T> id, T arg) => _messenger.Send(id, arg);
        public void Send<T0, T1>(in MsgId<T0, T1> id, T0 arg0, T1 arg1) => _messenger.Send(id, arg0, arg1);
        public void Send<T0, T1, T2>(in MsgId<T0, T1, T2> id, T0 arg0, T1 arg1, T2 arg2) => _messenger.Send(id, arg0, arg1, arg2);
        public void Send<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, T0 arg0, T1 arg1, T2 arg2, T3 arg3) =>
            _messenger.Send(id, arg0, arg1, arg2, arg3);
        
        public TReturn Request<TReturn>(in MsgId<TReturn> id) => _messenger.Request(id);
        public TReturn Request<T0, TReturn>(in MsgId<T0, TReturn> id, T0 arg0) => _messenger.Request(id, arg0);
        public TReturn Request<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, T0 arg0, T1 arg1) => _messenger.Request(id, arg0, arg1);
        public TReturn Request<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, T0 arg0, T1 arg1, T2 arg2) =>
            _messenger.Request(id, arg0, arg1, arg2);

        public bool HasListener(in MsgId id) => _messenger.HasListener(id);
        public bool HasListener<T>(in MsgId<T> id) => _messenger.HasListener(id);
        public bool HasListener<T0, T1>(in MsgId<T0, T1> id) => _messenger.HasListener(id);
        public bool HasListener<T0, T1, T2>(in MsgId<T0, T1, T2> id) => _messenger.HasListener(id);
        public bool HasListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id) => _messenger.HasListener(id);

        public void AddListener(in MsgId id, Action listener) => _messenger.AddListener(id, listener);
        public void AddListener<T>(in MsgId<T> id, Action<T> listener) => _messenger.AddListener(id, listener);
        public void AddListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener) =>
            _messenger.AddListener(id, listener);
        public void AddListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener) =>
            _messenger.AddListener(id, listener);
        public void AddListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener) =>
            _messenger.AddListener(id, listener);
        
        public void AddListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener) => _messenger.AddListener(id, listener);
        public void AddListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener) =>
            _messenger.AddListener(id, listener);
        public void AddListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener) =>
            _messenger.AddListener(id, listener);
        public void AddListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener) =>
            _messenger.AddListener(id, listener);
        
        public void RemoveListener(in MsgId id, Action listener) => _messenger.RemoveListener(id, listener);
        public void RemoveListener<T>(in MsgId<T> id, Action<T> listener) => _messenger.RemoveListener(id, listener);
        public void RemoveListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener) =>
            _messenger.RemoveListener(id, listener);
        public void RemoveListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener) =>
            _messenger.RemoveListener(id, listener);
        public void RemoveListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener) =>
            _messenger.RemoveListener(id, listener);
        
        public void RemoveListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener) => _messenger.RemoveListener(id, listener);
        public void RemoveListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener) =>
            _messenger.RemoveListener(id, listener);
        public void RemoveListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener) =>
            _messenger.RemoveListener(id, listener);
        public void RemoveListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener) =>
            _messenger.RemoveListener(id, listener);

        void IListenerObjectDeregistration.RemoveListener(int hash, object listener) =>
            ((IListenerObjectDeregistration)_messenger).RemoveListener(hash, listener);
    }
}