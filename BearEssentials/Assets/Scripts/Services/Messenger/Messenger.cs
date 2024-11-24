using System;
using System.Collections.Generic;
using Bears.Core.MessengerInternal;

namespace Bears.Core
{
    /// <summary>
    /// Simple messenger class 
    /// </summary>
    public class Messenger : IMessenger
    {
        private const int _InitPoolCap = 4;
        
        private readonly Dictionary<int, IMessageEvent> _listeners = new Dictionary<int, IMessageEvent>();
        private readonly Dictionary<Type, IListenerListPool> _listenerListPools = new Dictionary<Type, IListenerListPool>();

        public void Send(in MsgId id)
        {
            var typedEvent = GetMessageInvocation<IActionInvocation>(id.hash, id.kind, id.name);
            typedEvent?.Invoke();
        }

        public void Send<T>(in MsgId<T> id, T arg)
        {
            var typedEvent = GetMessageInvocation<IActionInvocation<T>>(id.hash, id.kind, id.name);
            typedEvent?.Invoke(arg);
        }

        public void Send<T0, T1>(in MsgId<T0, T1> id, T0 arg0, T1 arg1)
        {
            var typedEvent = GetMessageInvocation<IActionInvocation<T0, T1>>(id.hash, id.kind, id.name);
            typedEvent?.Invoke(arg0, arg1);
        }

        public void Send<T0, T1, T2>(in MsgId<T0, T1, T2> id, T0 arg0, T1 arg1, T2 arg2)
        {
            var typedEvent = GetMessageInvocation<IActionInvocation<T0, T1, T2>>(id.hash, id.kind, id.name);
            typedEvent?.Invoke(arg0, arg1, arg2);
        }
        
        public void Send<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            var typedEvent = GetMessageInvocation<IActionInvocation<T0, T1, T2, T3>>(id.hash, id.kind, id.name);
            typedEvent?.Invoke(arg0, arg1, arg2, arg3);
        }

        public TReturn Request<TReturn>(in MsgId<TReturn> id)
        {
            var typedEvent = GetMessageInvocation<IFuncInvocation<TReturn>>(id.hash, id.kind, id.name);
            return typedEvent.Invoke();
        }

        public TReturn Request<TArg, TReturn>(in MsgId<TArg, TReturn> id, TArg arg)
        {
            var typedEvent = GetMessageInvocation<IFuncInvocation<TArg, TReturn>>(id.hash, id.kind, id.name);
            return typedEvent.Invoke(arg);
        }

        public TReturn Request<TArg0, TArg1, TReturn>(in MsgId<TArg0, TArg1, TReturn> id, TArg0 arg0, TArg1 arg1)
        {
            var typedEvent = GetMessageInvocation<IFuncInvocation<TArg0, TArg1, TReturn>>(id.hash, id.kind, id.name);
            return typedEvent.Invoke(arg0, arg1);
        }
        
        public TReturn Request<TArg0, TArg1, TArg2, TReturn>(in MsgId<TArg0, TArg1, TArg2, TReturn> id, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        {
            var typedEvent = GetMessageInvocation<IFuncInvocation<TArg0, TArg1, TArg2, TReturn>>(id.hash, id.kind, id.name);
            return typedEvent.Invoke(arg0, arg1, arg2);
        }

        private TEvent GetMessageInvocation<TEvent>(int hash, MsgKind kind, string name) where TEvent : class
        {
            _listeners.TryGetValue(hash, out IMessageEvent msgEvent);
            
            if (kind is MsgKind.Command or MsgKind.Request && (msgEvent == null || msgEvent.Count == 0))
            {
                throw new CommandMessageException($"No {kind} listener \"{name}\" is registered yet.");
            }
            
            if (msgEvent == null)
            {
                return null;
            }
            
            if (!(msgEvent is TEvent typedEvent))
            {
                throw new ArgumentException($"Message id type conflicts with in-use id \"{name}\".");
            }

            return typedEvent;
        }

        public bool HasListener(in MsgId id) => HasListener(id.hash);
        public bool HasListener<T>(in MsgId<T> id) => HasListener(id.hash);
        public bool HasListener<T0, T1>(in MsgId<T0, T1> id) => HasListener(id.hash);
        public bool HasListener<T0, T1, T2>(in MsgId<T0, T1, T2> id) => HasListener(id.hash);
        public bool HasListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id) => HasListener(id.hash);

        private bool HasListener(int idHash)
        {
            if (_listeners.TryGetValue(idHash, out IMessageEvent msgEvent))
            {
                return msgEvent.Count > 0;
            }

            return false;
        }

        public void AddListener(in MsgId id, Action listener)
        {
            AddListener<MsgId, Action>(id.hash, id.kind, id.name, listener, 
                pool => pool == null ? new CommandEvent() : new NotificationEvent(pool));
        }

        public void AddListener<T>(in MsgId<T> id, Action<T> listener)
        {
            AddListener<MsgId<T>, Action<T>>(id.hash, id.kind, id.name, listener, 
                pool => pool == null ? new CommandEvent<T>() : new NotificationEvent<T>(pool));
        }

        public void AddListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener)
        {
            AddListener<MsgId<T0, T1>, Action<T0, T1>>(id.hash, id.kind, id.name, listener, 
                pool =>pool == null ? new CommandEvent<T0, T1>() : new NotificationEvent<T0, T1>(pool));
        }
        
        public void AddListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener)
        {
            AddListener<MsgId<T0, T1, T2>, Action<T0, T1, T2>>(id.hash, id.kind, id.name, listener, 
                pool => pool == null ? new CommandEvent<T0, T1, T2>() : new NotificationEvent<T0, T1, T2>(pool));
        }
        
        public void AddListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener)
        {
            AddListener<MsgId<T0, T1, T2, T3>, Action<T0, T1, T2, T3>>(id.hash, id.kind, id.name, listener, 
                pool => pool == null ? new CommandEvent<T0, T1, T2, T3>() : new NotificationEvent<T0, T1, T2, T3>(pool));
        }
        
        public void AddListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener)
        {
            AddListener<MsgId<TReturn>, Func<TReturn>>(id.hash, id.kind, id.name, listener,
                _ => new RequestEvent<TReturn>());
        }
        
        public void AddListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener)
        {
            AddListener<MsgId<T, TReturn>, Func<T, TReturn>>(id.hash, id.kind, id.name, listener,
                _ => new RequestEvent<T, TReturn>());
        }
        
        public void AddListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener)
        {
            AddListener<MsgId<T0, T1, TReturn>, Func<T0, T1, TReturn>>(id.hash, id.kind, id.name, listener,
                _ => new RequestEvent<T0, T1, TReturn>());
        }
        
        public void AddListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener)
        {
            AddListener<MsgId<T0, T1, T2, TReturn>, Func<T0, T1, T2, TReturn>>(id.hash, id.kind, id.name, listener,
                _ => new RequestEvent<T0, T1, T2, TReturn>());
        }

        private void AddListener<TMsgId, TAction>(int hash, MsgKind kind, string name, TAction listener, Func<ListenerListPool<TAction>, IMessageEvent<TAction>> createEvent) where TAction : Delegate
        {
            if (!_listeners.TryGetValue(hash, out IMessageEvent msgEvent))
            {
                var actionPool = kind == MsgKind.Notification ? GetListenerListPool<TMsgId, TAction>() : null;
                msgEvent = createEvent(actionPool);
                _listeners.Add(hash, msgEvent);
            }
            
            if (!(msgEvent is IMessageEvent<TAction> typedEvent))
            {
                throw new ArgumentException($"Message id type conflicts with in-use id \"{name}\".");
            }
            
            if (kind is MsgKind.Command or MsgKind.Request && typedEvent.Count > 0)
            {
                throw new Exception($"{kind} listener \"{name}\" already registered. No more than one {kind} listener is allowed.");
            }
            
            typedEvent.AddListener(listener);
        }

        private ListenerListPool<TAction> GetListenerListPool<TMsgId, TAction>()
        {
            ListenerListPool<TAction> pool;

            _listenerListPools.TryGetValue(typeof(TMsgId), out IListenerListPool poolObject);
            
            if (poolObject == null)
            {
                pool = new ListenerListPool<TAction>(_InitPoolCap);
                _listenerListPools.Add(typeof(TMsgId), pool);
            }
            else
            {
                pool = poolObject as ListenerListPool<TAction>;
            }
            
            return pool;
        }

        public void RemoveListener(in MsgId id, Action listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T>(in MsgId<T> id, Action<T> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<TReturn>(in MsgId<TReturn> id, Func<TReturn> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T, TReturn>(in MsgId<T, TReturn> id, Func<T, TReturn> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T0, T1, TReturn>(in MsgId<T0, T1, TReturn> id, Func<T0, T1, TReturn> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }
        
        public void RemoveListener<T0, T1, T2, TReturn>(in MsgId<T0, T1, T2, TReturn> id, Func<T0, T1, T2, TReturn> listener)
        {
            RemoveListener(id.hash, id.name, listener);
        }

        private void RemoveListener<TAction>(int hash, string name, TAction listener) where TAction : Delegate
        {
            if (!_listeners.TryGetValue(hash, out IMessageEvent msgEvent))
            {
                return;
            }
            
            if (!(msgEvent is IMessageEvent<TAction> typedEvent))
            {
                throw new ArgumentException($"Message id type conflicts with in-use id \"{name}\".");
            }
            
            typedEvent.RemoveListener(listener);
        }

        void IListenerObjectDeregistration.RemoveListener(int hash, object listener)
        {
            if (!_listeners.TryGetValue(hash, out IMessageEvent msgEvent))
            {
                return;
            }
            
            msgEvent.RemoveListener(listener);
        }
    }
}