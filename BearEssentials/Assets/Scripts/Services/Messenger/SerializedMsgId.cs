using System;
using UnityEngine;

namespace Bears.Core
{
    [Serializable]
    public class SerializedMsgId
    {
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField, HideInInspector] private int _hash;
        public int Hash => _hash;
        [SerializeField] private MsgKind _kind;
        public MsgKind Kind => _kind;
        
        public SerializedMsgId()
        {
            _kind = MsgKind.Notification;
        }
        
        public SerializedMsgId(string name, MsgKind kind)
        {
            _name = name;
            _hash = name.GetHashCode();
            _kind = kind;
        }
        
        public MsgId ToMsgId() => new MsgId(this);
    }
    
    [Serializable]
    public abstract class SerializedMsgId<T>
    {
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField, HideInInspector] private int _hash;
        public int Hash => _hash;
        [SerializeField] private MsgKind _kind;
        public MsgKind Kind => _kind;

        public SerializedMsgId()
        {
            _kind = MsgKind.Notification;
        }
        
        public SerializedMsgId(string name, MsgKind kind)
        {
            _name = name;
            _hash = name.GetHashCode();
            _kind = kind;
        }
        
        public MsgId<T> ToMsgId() => new MsgId<T>(this);
    }
    
    [Serializable]
    public abstract class SerializedMsgId<T0, T1>
    {
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField, HideInInspector] private int _hash;
        public int Hash => _hash;
        [SerializeField] private MsgKind _kind;
        public MsgKind Kind => _kind;
        
        public SerializedMsgId()
        {
            _kind = MsgKind.Notification;
        }
        
        public SerializedMsgId(string name, MsgKind kind)
        {
            _name = name;
            _hash = name.GetHashCode();
            _kind = kind;
        }
        
        public MsgId<T0, T1> ToMsgId() => new MsgId<T0, T1>(this);
    }
    
    [Serializable]
    public abstract class SerializedMsgId<T0, T1, T2>
    {
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField, HideInInspector] private int _hash;
        public int Hash => _hash;
        [SerializeField] private MsgKind _kind;
        public MsgKind Kind => _kind;
        
        public SerializedMsgId()
        {
            _kind = MsgKind.Notification;
        }
        
        public SerializedMsgId(string name, MsgKind kind)
        {
            _name = name;
            _hash = name.GetHashCode();
            _kind = kind;
        }

        public MsgId<T0, T1, T2> ToMsgId() => new MsgId<T0, T1, T2>(this);
    }
    
    [Serializable]
    public abstract class SerializedMsgId<T0, T1, T2, T3>
    {
        [SerializeField] private string _name;
        public string Name => _name;
        [SerializeField, HideInInspector] private int _hash;
        public int Hash => _hash;
        [SerializeField] private MsgKind _kind;
        public MsgKind Kind => _kind;
        
        public SerializedMsgId()
        {
            _kind = MsgKind.Notification;
        }
        
        public SerializedMsgId(string name, MsgKind kind)
        {
            _name = name;
            _hash = name.GetHashCode();
            _kind = kind;
        }

        public MsgId<T0, T1, T2, T3> ToMsgId() => new MsgId<T0, T1, T2, T3>(this);
    }
}

