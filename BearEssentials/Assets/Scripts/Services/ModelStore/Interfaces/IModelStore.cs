using JetBrains.Annotations;

namespace Bears.Core
{
    [PublicAPI]
    public interface IModelStore
    {
        /// <summary>
        /// Adds a model by key
        /// </summary>
        void AddModel<T>(string key, T model) where T : IModel;
        /// <summary>
        /// Removes a model by key
        /// </summary>
        void RemoveModel(string key);
        /// <summary>
        /// Returns a model of a type by key
        /// </summary>
        T GetModel<T>(string key) where T : IModel;
        
        /// <summary>
        /// Adds a default model for a type. Default models use a key based on the model's type.
        /// Technically, they can also be added/removed/retrieved by key because all models are stored in a single dictionary.
        /// </summary>
        void AddModel<T>(T model) where T : IModel;
        /// <summary>
        /// Remove the default model for a type
        /// </summary>
        void RemoveModel<T>() where T : IModel;
        /// <summary>
        /// Returns the default model for a type
        /// </summary>
        T GetModel<T>() where T : IModel;
    }
}