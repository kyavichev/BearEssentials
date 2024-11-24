using System;
using System.Collections.Generic;

namespace Bears.Core
{
    public class ModelStore : IModelStore
    {
        private readonly Dictionary<string, IModel> _models = new Dictionary<string, IModel>();

        public void AddModel<T>(string key, T model) where T : IModel
        {
            _models.Add(key, model);
        }

        public void RemoveModel(string key)
        {
            _models.Remove(key);
        }

        public T GetModel<T>(string key) where T : IModel
        {
            if (!_models.TryGetValue(key, out IModel model))
            {
                return default;
            }

            if (!(model is T typeModel))
            {
                throw new Exception($"Type mismatch for model at key {key}");
            }

            return typeModel;
        }
    }
}