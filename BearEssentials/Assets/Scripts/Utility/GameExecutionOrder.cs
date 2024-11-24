namespace Bears.Core.Internal
{
    public static class GameExecutionOrder
    {
        public const int Service = -10000;
        public const int ServicesAccess = -9000;
        public const int ModelRegistration = -8000;
        public const int Initializer = -7000;
        public const int Early = -1000;
        public const int Default = 0;
    }
}