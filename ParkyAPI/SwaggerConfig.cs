using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace ParkyAPI
{
    public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        public SwaggerConfig(IApiVersionDescriptionProvider provider) => _apiVersionDescriptionProvider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName, new OpenApiInfo()
                {
                    Title = $"Parky API {item.ApiVersion}",
                    Version = item.ApiVersion.ToString()
                });
            }
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var cmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(cmlPath);
        }
    }
}
