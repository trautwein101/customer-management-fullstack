namespace CustomerManagement.Api.Extensions
{
    public static class CorsExtensions
    {
        public const string FrontendCorsPolicy = "FrontendCorsPolicy";

        public static IServiceCollection AddFrontendCors(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigins =
                configuration.GetSection("Cors:AllowedOrigins")
                .Get<string[]>()
                ?? Array.Empty<string>();

            services.AddCors(options =>
            {
                options.AddPolicy(FrontendCorsPolicy, policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

    }
}
