using FollettSFTP.Controller;
using FollettSFTP.Directories.BB;
using FollettSFTP.Interfaces;
using FollettSFTP.Managers;
using FollettSFTP.DI;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(FollettSFTP.Startup))]

namespace FollettSFTP
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IProcessASN, ProcessASN>();
            builder.Services.AddScoped<IBB, BB>();
            builder.Services.AddScoped<ISendPayload, SendPayload>();

        }


    }
}
