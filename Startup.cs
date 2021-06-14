using M365UK.Functions.Helpers.Auth;
using M365UK.Functions.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(M365UK.Functions.Startup))]

namespace M365UK.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            
            builder.Services.AddOptions<AppSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("AppSettings").Bind(settings);
            });

            builder.Services.AddSingleton<IAuthProvider, ADALAuthProvider>();
            builder.Services.AddSingleton<IAuthProvider, MSALAuthProvider>();
            builder.Services.AddSingleton<IAuthProvider, AppAuthLibAuthProvider>();
            builder.Services.AddSingleton<IAuthProvider, AzureIdentityAuthProvider>();
        }
    }
}