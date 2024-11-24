using System;
using Bears.Core.Internal;
using UnityEngine;

namespace Bears.Core
{
    /// <summary>
    /// An abstract proxy class with a reference to a Unity asset that registers itself to the model store service
    /// </summary>
    [DefaultExecutionOrder(GameExecutionOrder.ModelRegistration - 10)]
    public abstract class AssetModel<T> : MonoBehaviour, IModel where T : UnityEngine.Object
    {
        [SerializeField] protected T _asset;
        public T Asset => _asset;
        
        /// <summary>
        /// A messenger for sending model notifications
        /// </summary>
        protected IMessenger Messenger { get; private set; }

        protected abstract string GetStoreKey();


        protected void Awake()
        {
            if (string.IsNullOrEmpty(GetStoreKey()))
            {
                Log.Shared.Warn(CommonChannels.Setup, $"Store key for asset model {name} is null or empty.");
                return;
            }
            
            if (_asset == null)
            {
                Log.Shared.Warn(CommonChannels.Setup, $"Asset for asset model {name} is null.");
                return;
            }

            var store = Services.Get<IModelStoreService>();
            if (store == null)
            {
                throw new Exception("Model store service cannot be found. Service access may be missing manager reference.");
            }
            
            store.AddModel(GetStoreKey(), this);
            Messenger = Services.Get<IMessengerService>();
        }

        protected void OnDestroy()
        {
            var store = Services.Get<IModelStoreService>();
            store?.RemoveModel(GetStoreKey());
        }
    }
}