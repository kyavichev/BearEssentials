namespace Bears.Core
{
    public interface IService
    {
        /// <summary>
        /// Check before using the service in MonoBehaviour OnDisable and OnDestroy implementations
        /// </summary>
        bool Available { get; }
        void OnRegister();
        void OnDeregister();
    }
}