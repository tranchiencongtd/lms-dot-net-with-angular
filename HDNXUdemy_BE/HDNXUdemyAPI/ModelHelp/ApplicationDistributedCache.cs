namespace HDNXUdemyAPI.ModelHelp
{
    public static class ApplicationDistributedCache
    {
        public static void ApplicationDistributedConfigulation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(
                option =>
                {
                    option.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                    {
                        User = configuration["Redis:User"] ?? string.Empty,
                        Password = configuration["Redis:Password"] ?? string.Empty,
                        AllowAdmin = true,
                        EndPoints =
                        {
                            {configuration["Redis:Host"], int.Parse(configuration["Redis:Port"]) }
                        },

                        AbortOnConnectFail = false
                    };
                });
        }
    }
}