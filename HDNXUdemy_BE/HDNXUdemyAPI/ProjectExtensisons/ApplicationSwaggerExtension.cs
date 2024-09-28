using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HDNXUdemyAPI.ProjectExtensisons
{
    /// <summary>
    /// ApplicationSwaggerExtension
    /// </summary>
    public static class ApplicationSwaggerExtension
    {
        /// <summary>
        /// AddSwashbuckleSwagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwashbuckleSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", System.Array.Empty<string>() },
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Login with your bearer authentication token. e.g. Bearer <auth-token>",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
            });
        }

        /// <summary>
        /// UseSwashbuckleSwagger
        /// </summary>
        /// <param name="application"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public static void UseSwashbuckleSwagger(this IApplicationBuilder application, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            application.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"{httpReq.Scheme}://{httpReq.Host}", Description = "Localhost Server" } };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            application.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    options.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
                }
            });
        }

        /// <summary>
        /// SwaggerConfigureOptions
        /// </summary>
        public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider _provider;

            /// <summary>
            /// SwaggerConfigureOptions
            /// </summary>
            /// <param name="provider"></param>
            public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

            /// <summary>
            /// Configure
            /// </summary>
            /// <param name="options"></param>
            public void Configure(SwaggerGenOptions options)
            {
                foreach (var desc in _provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Elearning API",
                        Version = desc.ApiVersion.ToString(),
                    });
                }
            }
        }
    }
}