using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RateAmData;
using RateAmData.Repositories;
using RateAmLib.Services;


//http://localhost:5002/index.html swagger

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Set the desired port here (e.g., 5002)
    serverOptions.ListenAnyIP(5002);
});
/*var builderConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("C:\\Users\\khaza\\source\\repos\\RatesAm\\RateAmTerminal\\appsettings.json")
    .Build();*/

var builderConfig = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rates Api", Version = "v1" });
});

// my services in the container.
builder.Services.AddSingleton<IConfiguration>(builderConfig);
builder.Services.AddScoped<RateAmDataContext>();
builder.Services.AddScoped<IRatesRepository, RatesRepository>();
builder.Services.AddScoped<IBanksRepository, BanksRepository>();
builder.Services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();


builder.Services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Formatting = Formatting.Indented;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        };
                        options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.String; // Preserve trailing zeros in decimal values
                    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",

        builder => builder.WithOrigins("http://localhost:4200") // Adjust the URL as needed
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //options.SwaggerEndpoint("/swagger/index.html", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowConsoleApp");
app.UseCors("AllowSpecificOrigin");
//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapSwagger().RequireAuthorization();
app.MapControllers();

app.Run();
