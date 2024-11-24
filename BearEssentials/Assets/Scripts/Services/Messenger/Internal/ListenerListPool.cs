namespace Bears.Core.MessengerInternal
{
    internal interface IListenerListPool
    {
        
    }
    
    internal class ListenerListPool<T> : Pool<PooledList<T>>, IListenerListPool 
    {
        public ListenerListPool(int initSize) : base(initSize) { }
    }
}