namespace Bears.Core.ModelStoreInternal
{
    public class ModelStoreService : ServiceComponent<IModelStoreService, ModelStoreService>, IModelStoreService
    {
        private readonly ModelStore _store = new ModelStore();

        protected override void OnRegister()
        {
            
        }

        protected override void OnDeregister()
        {
            
        }

        public void AddModel<T>(string key, T model) where T : IModel =>
            _store.AddModel(key, model);

        public void RemoveModel(string key) =>
            _store.RemoveModel(key);

        public T GetModel<T>(string key) where T : IModel =>
            _store.GetModel<T>(key);
    }
}