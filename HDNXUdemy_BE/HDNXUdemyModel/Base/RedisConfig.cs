namespace HDNXUdemyModel.Base
{
    public class RedisConfig
    {
        public RedisConnect? RedisConnect { get; set; }
    }

    public class RedisConnect
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ScronJobSetting { get; set; }

        public string? JobAssgimentDelivery { get; set; }

        public bool IsEnableDashboard { get; set; }
    }
}