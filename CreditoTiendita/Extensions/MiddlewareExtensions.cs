using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CreditoTiendita.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Credito Tiendita API",
                    Version = "v1",
                    Description = "Credito Tiendita RESTful API",
                    Contact = new OpenApiContact { Name = "JEPRAV Team" }
                });

                c.EnableAnnotations();
            });
            return services;
        }
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "Credito Tiendita API V1");
                c.RoutePrefix = "api-docs/v1";
            });
            return app;
        }
    }
}
