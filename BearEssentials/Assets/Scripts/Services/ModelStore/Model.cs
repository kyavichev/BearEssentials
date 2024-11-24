namespace Bears.Core
{
    public abstract class Model : IModel
    {
        protected IMessenger Messenger { get; }

        protected Model(IMessenger messenger)
        {
            Messenger = messenger;
        }
    }
}