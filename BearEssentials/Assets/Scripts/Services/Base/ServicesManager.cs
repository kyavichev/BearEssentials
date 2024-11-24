using System;
using System.Collections.Generic;
using Bears.Core.Internal;
using UnityEngine;

namespace Bears.Core.ServicesInternal
{
    [DefaultExecutionOrder(GameExecutionOrder.Service - 10)]
    internal class ServicesManager : MonoBehaviour
    {
        private readonly Dictionary<Type, IService> _services = new();
        
        internal static bool CurrentExists { get; private set; }
        internal static ServicesManager Current { get; private set; }

        private void Awake()
        {
            if (Current != null)
            {
                Destroy(this);
                return;
            }

            CurrentExists = true;
            Current = this;
        }

        private void OnDestroy()
        {
            if (Current != this)
            {
                return;
            }
            
            foreach ((Type type, IService service) in _services)
            {
                if (service is MonoBehaviour behaviour && behaviour == null)
                {
                    continue;
                }
                
                service.OnDeregister();
            }
            _services.Clear();

            CurrentExists = false;
            Current = null;
        }
        
        public T Get<T>() where T : class, IService
        {
            if (!_services.TryGetValue(typeof(T), out IService service) || !(service is T typedService) ||
                (service is MonoBehaviour monoService && monoService == null))
            {
                return null;
            }

            return typedService;
        }

        public void RegisterService<T>(T service) where T : class, IService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            
            _services.Add(typeof(T), service);
            service.OnRegister();
        }

        public void DeregisterService<T>() where T : class, IService
        {
            T service = Get<T>();
            if (service == null)
            {
                return;
            }

            _services.Remove(typeof(T));
            service.OnDeregister();
        }
    }
}