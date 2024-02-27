using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RateAmLib.Services;
using RatesTerminal;

var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .Build();
var connectionString = builder.GetConnectionString("RateDb");

var services = new ServiceCollection();
//DI.ConfigureServices(services, builder);
var host = DI.CreateHostBuilder(builder).Build();

//var serviceProvider = services.BuildServiceProvider();

//var workerService = serviceProvider.GetRequiredService<WorkerService>();

/*        using (var cts = new CancellationTokenSource())
        {
            // Handle Ctrl+C to stop the application
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true; // Prevent the application from closing immediately
                cts.Cancel();
            };

            workerService.StartAsync(cts.Token).GetAwaiter().GetResult();

        }*/
using (var serviceScope = host.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    try
    {
        var workerService = serviceProvider.GetRequiredService<WorkerService>();
        // Start your worker service or any other initialization logic here

        using (var cts = new CancellationTokenSource())
        {
            // Handle Ctrl+C to stop the application
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true; // Prevent the application from closing immediately
                cts.Cancel();
            };

            workerService.StartAsync(cts.Token).GetAwaiter().GetResult();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

}