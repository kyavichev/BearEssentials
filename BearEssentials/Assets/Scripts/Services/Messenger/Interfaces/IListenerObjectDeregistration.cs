namespace Bears.Core
{
    public interface IListenerObjectDeregistration
    {
        void RemoveListener(int hash, object listener);
    }
}