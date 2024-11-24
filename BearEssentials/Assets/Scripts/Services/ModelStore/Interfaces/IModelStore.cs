namespace Bears.Core
{
    public interface IModelStore
    {
        void AddModel<T>(string key, T model) where T : IModel;
        void RemoveModel(string key);
        T GetModel<T>(string key) where T : IModel;
    }
}