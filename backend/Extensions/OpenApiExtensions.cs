using Microsoft.OpenApi;

namespace CustomerManagement.Api.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApiWithJwt(
            this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer(
                    (document, context, cancellationToken) =>
                    {
                        document.Components ??= new OpenApiComponents();

                        document.Components.SecuritySchemes ??=
                            new Dictionary<string, IOpenApiSecurityScheme>();

                        document.Components.SecuritySchemes["Bearer"] =
                            new OpenApiSecurityScheme
                            {
                                Type = SecuritySchemeType.Http,
                                Scheme = "bearer",
                                BearerFormat = "JWT",
                                Description = "Enter your JWT access token."
                            };

                        document.Security =
                        [
                            new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecuritySchemeReference(
                                "Bearer", document)] = []
                        }
                        ];

                        return Task.CompletedTask;
                    });
            });

            return services;
        }
    }
}
