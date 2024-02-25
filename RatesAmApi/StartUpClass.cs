using Microsoft.OpenApi.Models;
using RateAmData.Repositories;
using RateAmData;
using RateAmLib.Services;

namespace RatesAmApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rates Api", Version = "v1" });
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<RateAmDataContext>();
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IBanksRepository, BanksRepository>();
            services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<ICurrencyService, CurrencyService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowConsoleApp",
                    builder => builder.WithOrigins("http://localhost:5002") // Adjust the URL as needed
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("AllowConsoleApp");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
