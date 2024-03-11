using Microsoft.Extensions.Hosting;
using RateAmData.Repositories;
using RateAmData;
using RateAmLib.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RateAmLib.Utils.Abtract;
using RateAmLib.Utils;

namespace RatesTerminal
{
    public class DI
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services, IConfigurationRoot builderConfig)
        {

            services.AddSingleton<IConfiguration>(builderConfig);
            services.AddDbContext<RateAmDataContext>();
            services.AddScoped<RedisCache>();
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<IBanksRepository, BanksRepository>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IWebClient, SeleniumWebClient>();
            services.AddScoped<IXmlParser, XmlDocumentParser>();
            services.AddScoped<IHtmlTableParser, HtmlTableParser>();
            services.AddScoped<ITableToObjectParser, TableToObjectParser>();
            services.AddScoped<WorkerService>();

            return services;
        }

        public static IHostBuilder CreateHostBuilder(IConfigurationRoot builderConfig) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IConfiguration>(builderConfig);
                    services.AddDbContext<RateAmDataContext>();
                    services.AddScoped<RedisCache>();
                    services.AddScoped<IRatesRepository, RatesRepository>();
                    services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
                    services.AddScoped<IBanksRepository, BanksRepository>();
                    services.AddScoped<IRateService, RateService>();
                    services.AddScoped<IBankService, BankService>();
                    services.AddScoped<BankService>();
                    services.AddScoped<CurrencyService>();
                    services.AddScoped<ICurrencyService, CurrencyService>();
                    services.AddScoped<IWebClient, SeleniumWebClient>();
                    services.AddScoped<IXmlParser, XmlDocumentParser>();
                    services.AddScoped<IHtmlTableParser, HtmlTableParser>();
                    services.AddScoped<ITableToObjectParser, TableToObjectParser>();
                    services.AddScoped<WorkerService>();

                });
    }

}

