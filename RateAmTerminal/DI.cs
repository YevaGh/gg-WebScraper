using Microsoft.Extensions.Hosting;
using RateAmData.Repositories;
using RateAmData;
using RateAmLib.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RateAmLib.Utils.Abtract;
using RateAmLib.Utils;

namespace RateAmTerminal
{
    public class DI
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot builderConfig)
        {

            services.AddSingleton<IConfiguration>(builderConfig);
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IWebClient, SeleniumWebClient>();
            services.AddScoped<IXmlParser, XmlDocumentParser>();
            services.AddScoped<IHtmlTableParser, HtmlTableParser>();
            services.AddScoped<ITableToObjectParser, TableToObjectParser>();
            services.AddDbContext<RateAmDataContext>();
            services.AddScoped<WorkerService>();

            return services;
        }

        public static IHostBuilder CreateHostBuilder(IConfigurationRoot builderConfig) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IConfiguration>(builderConfig);
                    services.AddScoped<IRatesRepository, RatesRepository>();
                    services.AddScoped<IRateService, RateService>();
                    services.AddScoped<IWebClient, SeleniumWebClient>();
                    services.AddScoped<IXmlParser, XmlDocumentParser>();
                    services.AddScoped<IHtmlTableParser, HtmlTableParser>();
                    services.AddScoped<ITableToObjectParser, TableToObjectParser>();
                    services.AddDbContext<RateAmDataContext>();
                    services.AddScoped<WorkerService>();
                });
    }

}

