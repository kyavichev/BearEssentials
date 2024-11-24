using System;

namespace Bears.Core
{
    public readonly struct MsgId
    {
        public readonly string name;
        public readonly int hash;
        public readonly MsgKind kind;

        public MsgId(string name, MsgKind kind)
        {
            if (kind == MsgKind.Request)
            {
                throw new Exception($"Plain MsgId type does not have a type argument to return for {kind} kind.");
            }
            
            this.name = name;
            this.hash = name.GetHashCode();
            this.kind = kind;
        }
        
        public MsgId(SerializedMsgId other)
        {
            if (other.Kind == MsgKind.Request)
            {
                throw new Exception($"Plain MsgId type does not have a type argument to return for {other.Kind} kind.");
            }
            
            this.name = other.Name;
            this.hash = other.Hash;
            this.kind = other.Kind;
        }

        public override int GetHashCode()
        {
            return hash;
        }

        public override string ToString()
        {
            return $"{name} ({hash})";
        }

        public static implicit operator MsgId(SerializedMsgId other) => new (other);

        public static MsgId Note(string name) => new (name, MsgKind.Notification);
        public static MsgId Cmd(string name) => new (name, MsgKind.Command);
    }
    
    public readonly struct MsgId<T>
    {
        public readonly string name;
        public readonly int hash;
        public readonly MsgKind kind;

        public MsgId(string name, MsgKind kind)
        {
            this.name = name;
            this.hash = name.GetHashCode();
            this.kind = kind;
        }
        
        public MsgId(SerializedMsgId<T> other)
        {
            this.name = other.Name;
            this.hash = other.Hash;
            this.kind = other.Kind;
        }

        public override int GetHashCode()
        {
            return hash;
        }
        
        public override string ToString()
        {
            return $"{name} ({hash})";
        }

        public static implicit operator MsgId<T>(SerializedMsgId<T> other) => new (other);
        
        public static MsgId<T> Note(string name) => new (name, MsgKind.Notification);
        public static MsgId<T> Cmd(string name) => new (name, MsgKind.Command);
        public static MsgId<T> Req(string name) => new (name, MsgKind.Request);
    }
    
    public readonly struct MsgId<T0, T1>
    {
        public readonly string name;
        public readonly int hash;
        public readonly MsgKind kind;

        public MsgId(string name, MsgKind kind)
        {
            this.name = name;
            this.hash = name.GetHashCode();
            this.kind = kind;
        }
        
        public MsgId(SerializedMsgId<T0, T1> other)
        {
            this.name = other.Name;
            this.hash = other.Hash;
            this.kind = other.Kind;
        }

        public override int GetHashCode()
        {
            return hash;
        }
        
        public override string ToString()
        {
            return $"{name} ({hash})";
        }

        public static implicit operator MsgId<T0, T1>(SerializedMsgId<T0, T1> other) => new (other);
        
        public static MsgId<T0, T1> Note(string name) => new (name, MsgKind.Notification);
        public static MsgId<T0, T1> Cmd(string name) => new (name, MsgKind.Command);
        public static MsgId<T0, T1> Req(string name) => new (name, MsgKind.Request);
    }
    
    public readonly struct MsgId<T0, T1, T2>
    {
        public readonly string name;
        public readonly int hash;
        public readonly MsgKind kind;

        public MsgId(string name, MsgKind kind)
        {
            this.name = name;
            this.hash = name.GetHashCode();
            this.kind = kind;
        }
        
        public MsgId(SerializedMsgId<T0, T1, T2> other)
        {
            this.name = other.Name;
            this.hash = other.Hash;
            this.kind = other.Kind;
        }

        public override int GetHashCode()
        {
            return hash;
        }
        
        public override string ToString()
        {
            return $"{name} ({hash})";
        }

        public static implicit operator MsgId<T0, T1, T2>(SerializedMsgId<T0, T1, T2> other) => new (other);
        
        public static MsgId<T0, T1, T2> Note(string name) => new (name, MsgKind.Notification);
        public static MsgId<T0, T1, T2> Cmd(string name) => new (name, MsgKind.Command);
        public static MsgId<T0, T1, T2> Req(string name) => new (name, MsgKind.Request);
    }
    
    public readonly struct MsgId<T0, T1, T2, T3>
    {
        public readonly string name;
        public readonly int hash;
        public readonly MsgKind kind;

        public MsgId(string name, MsgKind kind)
        {
            this.name = name;
            this.hash = name.GetHashCode();
            this.kind = kind;
        }
        
        public MsgId(SerializedMsgId<T0, T1, T2, T3> other)
        {
            this.name = other.Name;
            this.hash = other.Hash;
            this.kind = other.Kind;
        }

        public override int GetHashCode()
        {
            return hash;
        }
        
        public override string ToString()
        {
            return $"{name} ({hash})";
        }

        public static implicit operator MsgId<T0, T1, T2, T3>(SerializedMsgId<T0, T1, T2, T3> other) => new (other);
        
        public static MsgId<T0, T1, T2, T3> Note(string name) => new (name, MsgKind.Notification);
        public static MsgId<T0, T1, T2, T3> Cmd(string name) => new (name, MsgKind.Command);
        public static MsgId<T0, T1, T2, T3> Req(string name) => new (name, MsgKind.Request);
    }
}