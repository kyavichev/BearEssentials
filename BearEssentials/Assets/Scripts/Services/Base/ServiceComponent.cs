using Bears.Core.Internal;
using UnityEngine;


namespace Bears.Core
{
 
    /// <summary>
    /// An abstract class for MonoBehavior based services. Registers service on awake and de-registers on destroy
    /// </summary>
    /// <typeparam name="TInterface">The interface that the will be use to register to the services manager</typeparam>
    /// <typeparam name="TClass">The class that implements the registration interface</typeparam>
    [DefaultExecutionOrder(GameExecutionOrder.Service)]
    public abstract class ServiceComponent<TInterface, TClass> : MonoBehaviour, IService
        where TInterface : class, IService
        where TClass : ServiceComponent<TInterface, TClass>, TInterface
    {
        protected bool _available;
        
        private void Awake()
        {
            _available = true;
            ServicesRegistration.RegisterService<TInterface>((TClass)this);
        }

        private void OnDestroy()
        {
            ServicesRegistration.DeregisterService<TInterface>();
            _available = false;
        }

        bool IService.Available => Available;
        void IService.OnRegister() => OnRegister();
        void IService.OnDeregister() => OnDeregister();

        protected virtual bool Available => _available;
        protected abstract void OnRegister();
        protected abstract void OnDeregister();
    }
}