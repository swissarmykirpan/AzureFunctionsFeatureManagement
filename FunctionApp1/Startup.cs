using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using FunctionApp1;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddFeatureManagement()
                .AddFeatureFilter<PercentageFilter>();

            var config = new ConfigurationBuilder();
            var settings = config.Build();

            config.AddAzureAppConfiguration(options => {
                options.Connect(settings["ConnectionStrings:AppConfig"])
                    .UseFeatureFlags();
            });
        }
    }
}
