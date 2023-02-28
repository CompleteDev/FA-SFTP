using FollettSFTP.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollettSFTP.DI
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MyServerOptions>(config.GetSection(nameof(MyServerOptions)));
            return services;
        }
    }
}
