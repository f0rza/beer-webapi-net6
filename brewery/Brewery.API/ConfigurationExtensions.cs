using Brewery.Repositories;
using Brewery.Services;
using Microsoft.OpenApi.Models;

namespace Brewery.API
{
    internal static class ConfigurationExtensions
    {
        private const string APIVersion = "v1";

        public static void AddOpenApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(APIVersion, new OpenApiInfo
                {
                    Title = "Brewery API",
                    Description = "Add rating / retrieve list of beers",
                    Version = APIVersion
                });
            });
        }

        public static void UseOpenApiDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/{APIVersion}/swagger.json", $"Brewery API {APIVersion.ToUpper()}"); });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBeerService, BeerService>();
            services.AddScoped<IBeerStorageClient, BeerStorageClient>();
            services.AddScoped<ILocalFileRepository, LocalFileRepository>();
        }
    }
}
