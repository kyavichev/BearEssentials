using Bears.Core.ServicesInternal;

namespace Bears.Core
{
    public static class Services
    {
        /// <summary>
        /// Returns true if services are available for use. Returns false if outside of the services manager's Unity lifecycle.
        /// </summary>
        public static bool Available => ServicesManager.CurrentExists;
        
        /// <summary>
        /// Attempts to retrieve a service of a given type. Always returns null when <see cref="Available"/> is false
        /// </summary>
        public static T Get<T>() where T : class, IService => ServicesManager.CurrentExists ? ServicesManager.Current.Get<T>() : null;
    }
}