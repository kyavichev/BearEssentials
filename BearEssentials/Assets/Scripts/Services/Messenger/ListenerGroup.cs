using System;
using System.Collections.Generic;

namespace Bears.Core
{
    /// <summary>
    /// A class that simplifies de-registration of a group of message listeners
    /// </summary>
    public class ListenerGroup : IMessageRegistrar
    {
        private readonly IMessenger _messenger;
        private readonly Dictionary<int, List<object>> _listeners = new ();
        private readonly MsgKind _kind1;
        private readonly MsgKind _kind2;
        
        /// <summary>
        /// Constructor for a listener group that can only hold any kind of messages
        /// </summary>
        public ListenerGroup(IMessenger messenger)
        {
            _messenger = messenger;
            _kind1 = _kind2 = 0;
        }
        
        /// <summary>
        /// Constructor for a listener group that can only hold a specific kind of messages
        /// </summary>
        /// <param name="messenger">the messenger to use for registration</param>
        /// <param name="kind">the kind of messages to allow. pass 0 for no restrictions</param>
        public ListenerGroup(IMessenger messenger, MsgKind kind)
        {
            _messenger = messenger;
            _kind1 = _kind2 = kind;
        }
        
        /// <summary>
        /// Constructor for a listener group that can only hold two kinds of messages
        /// </summary>
        /// <param name="messenger">the messenger to use for registration</param>
        /// <param name="kind1">the kind of messages to allow. pass 0 for no restrictions</param>
        /// /// <param name="kind2">the kind of messages to allow. pass 0 for no restrictions</param>
        public ListenerGroup(IMessenger messenger, MsgKind kind1, MsgKind kind2)
        {
            _messenger = messenger;
            _kind1 = kind1;
            _kind2 = kind2;
        }

        private List<object> GetListenerList(int hash, bool createIfNull)
        {
            if (_listeners.TryGetValue(hash, out List<object> actions))
            {
                return actions;
            }

            if (!createIfNull)
            {
                return null;
            }
            
            actions = new List<object>();
            _listeners.Add(hash, actions);
            return actions;
        }
        public void AddListener(in MsgId id, Action listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }

        public void AddListener<T>(in MsgId<T> id, Action<T> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }

        public void AddListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }

        public void AddListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }
        
        public void AddListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }
        
        public void AddListener<TResult>(in MsgId<TResult> id, Func<TResult> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }
        
        public void AddListener<T, TResult>(in MsgId<T, TResult> id, Func<T, TResult> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }
        
        public void AddListener<T0, T1, TResult>(in MsgId<T0, T1, TResult> id, Func<T0, T1, TResult> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }
        
        public void AddListener<T0, T1, T2, TResult>(in MsgId<T0, T1, T2, TResult> id, Func<T0, T1, T2, TResult> listener)
        {
            TrackListener(id.name, id.hash, id.kind, listener);
            _messenger.AddListener(id, listener);
        }

        private void TrackListener(string name, int hash, MsgKind kind, object listener)
        {
            EnsureAddKind(name, kind);
            var actions = GetListenerList(hash, true);
            actions.Add(listener);
        }

        private void EnsureAddKind(string name, MsgKind kind)
        {
            if (_kind1 != 0 && kind != _kind1 && kind != _kind2)
            {
                throw new ArgumentException(_kind1 != _kind2
                    ? $"Message id {name} must be of kind {_kind1} or {_kind2} to be added to the listener group."
                    : $"Message id {name} must be of kind {_kind1} to be added to the listener group.");
            }
        }

        public void RemoveListener(in MsgId id, Action listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T>(in MsgId<T> id, Action<T> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1>(in MsgId<T0, T1> id, Action<T0, T1> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Action<T0, T1, T2> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Action<T0, T1, T2, T3> listener) => RemoveListener(id.hash, listener);
        
        public void RemoveListener<T>(in MsgId<T> id, Func<T> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1>(in MsgId<T0, T1> id, Func<T0, T1> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1, T2>(in MsgId<T0, T1, T2> id, Func<T0, T1, T2> listener) => RemoveListener(id.hash, listener);

        public void RemoveListener<T0, T1, T2, T3>(in MsgId<T0, T1, T2, T3> id, Func<T0, T1, T2, T3> listener) => RemoveListener(id.hash, listener);


        private void RemoveListener(int hash, object listener)
        {
            var actions = GetListenerList(hash, false);
            bool removed = actions?.Remove(listener) == true;
            
            if (removed)
            {
                _messenger.RemoveListener(hash, listener);
            }
        }

        public void RemoveAllListeners()
        {
            foreach ((int hash, List<object> listeners) in _listeners)
            {
                foreach (object listener in listeners)
                {
                    _messenger.RemoveListener(hash, listener);
                }
            }
            _listeners.Clear();
        }
    }
}