using System;
using Bears.Core.ServicesInternal;

namespace Bears.Core
{
    public static class ServicesRegistration
    {
        /// <summary>
        /// Register a service
        /// </summary>
        /// <exception cref="Exception">Throws when there is no current services manager.</exception>
        public static void RegisterService<T>(T service) where T : class, IService
        {
            if (!ServicesManager.CurrentExists)
            {
                throw new Exception("Service cannot be registered because there is no current manager.");
            }

            ServicesManager.Current.RegisterService(service);
        }
        
        /// <summary>
        /// Deregister a service. Does nothing when there is no current services manager.
        /// </summary>
        public static void DeregisterService<T>() where T : class, IService
        {
            if (!ServicesManager.CurrentExists)
            {
                return;
            }

            ServicesManager.Current.DeregisterService<T>();
        }
    }
}