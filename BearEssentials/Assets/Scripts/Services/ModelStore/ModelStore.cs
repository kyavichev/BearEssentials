using System;
using System.Collections.Generic;


namespace Bears.Core
{
    public class ModelStore : IModelStore
    {
        private const string _DefaultSubKey = "_DefaultByType_";
        
        /// <summary>
        /// Returns a default key for a given type
        /// </summary>
        public static string GetDefaultKey<T>() where T : IModel
        {
            return GetKey<T>(_DefaultSubKey);
        }

        /// <summary>
        /// Returns a key for the type with a sub key in the format {type.Namespace}.{type.Name}.{subKey}
        /// </summary>
        public static string GetKey<T>(string subKey) where T : IModel
        {
            var type = typeof(T);
            return $"{type.Namespace}.{type.Name}.{subKey}";
        }
        
        private readonly Dictionary<string, IModel> _models = new();

        public void AddModel<T>(string key, T model) where T : IModel
        {
            _models.Add(key, model);
        }

        public void AddModel<T>(T model) where T : IModel
        {
            _models.Add(GetDefaultKey<T>(), model);
        }

        public void RemoveModel(string key)
        {
            _models.Remove(key);
        }

        public void RemoveModel<T>() where T : IModel
        {
            _models.Remove(GetDefaultKey<T>());
        }

        public T GetModel<T>(string key) where T : IModel
        {
            if (!_models.TryGetValue(key, out IModel model))
            {
                return default;
            }

            if (model is not T typeModel)
            {
                throw new Exception($"Type mismatch for model at key {key}");
            }

            return typeModel;
        }

        public T GetModel<T>() where T : IModel
        {
            return GetModel<T>(GetDefaultKey<T>());
        }
    }
}