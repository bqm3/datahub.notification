namespace Lib.Middleware
{
    public class ProviderOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public class MicroServiceInfo
    {
        public string ServiceName { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Description { get; set; }

    }
    public class HealthCheckInfo
    {
        public string TypeName { get; set; }
        public string TypeHealthCheck { get; set; }
        public string Description { get; set; }
        public string UriConnectionString { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

    }
}
