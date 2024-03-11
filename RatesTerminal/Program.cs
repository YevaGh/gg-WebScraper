using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RateAmData;
using RateAmLib;
using RateAmLib.Services;
using RatesTerminal;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .Build();
        var connectionString = builder.GetConnectionString("RateDb");

        var services = new ServiceCollection();

        var host = DI.CreateHostBuilder(builder).Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;

            var context = serviceProvider.GetRequiredService<RateAmDataContext>();
            var serviceBank = serviceProvider.GetRequiredService<BankService>();
            var serviceCurr = serviceProvider.GetRequiredService<CurrencyService>();
            //context.Database.Migrate();
            context.Database.EnsureCreated();
            if(!serviceBank.GetAll().Result.Any()) {
            Seed(serviceBank,serviceCurr);
            }
            

            try
            {
                var workerService = serviceProvider.GetRequiredService<WorkerService>();
                
                context.Database.GetAppliedMigrations();
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
    }

    private static void Seed(IBankService serviceBank, ICurrencyService currencyService)
    {
        var banks = new Bank[] {
                   new Bank {BankId = 1 ,Name = "Fast Bank",          IconURL = "https://rate.am/images/organization/logo/767eaf3e45ae41bca8d7e4e481da6501.jpg"},
                   new Bank {BankId = 2 ,Name = "Unibank",            IconURL = "https://rate.am/images/organization/logo/9cf13d95c8214c7e989a242cc0772311.jpg"},
                   new Bank {BankId = 3 ,Name = "Acba bank",          IconURL = "https://rate.am/images/organization/logo/8203181c42c441a68f3a4cd769ab09c6.jpg"},
                   new Bank {BankId = 4 ,Name = "Artsakhbank",        IconURL = "https://rate.am/images/organization/logo/8.gif"},
                   new Bank {BankId = 5 ,Name = "VTB Bank (Armenia)", IconURL = "https://rate.am/images/organization/logo/2ae3fc783f014420a23da55d69552d90.gif"},
                   new Bank {BankId = 6 ,Name = "Evocabank",          IconURL = "https://rate.am/images/organization/logo/7d9ba737c27842de87bc5abe9e901525.png"},
                   new Bank {BankId = 7 ,Name = "Inecobank",          IconURL = "https://rate.am/images/organization/logo/13.gif"},
                   new Bank {BankId = 8 ,Name = "IDBank",             IconURL = "https://rate.am/images/organization/logo/170e82d5663b4d20ab76303baddfdec4.jpg"},
                   new Bank {BankId = 9 ,Name = "Byblos Bank Armenia",IconURL = "https://rate.am/images/organization/logo/9.gif"},
                   new Bank {BankId = 10,Name = "ArmSwissBank",       IconURL = "https://rate.am/images/organization/logo/aa6589e16c574e9981e3bc75719c6e3e.gif"},
                   new Bank {BankId = 11,Name = "Ardshinbank",        IconURL = "https://rate.am/images/organization/logo/6.gif"},
                   new Bank {BankId = 12,Name = "AraratBank",         IconURL = "https://rate.am/images/organization/logo/b5700b0d821c412a8a69a22f5ce350c0.jpg"},
                   new Bank {BankId = 13,Name = "HSBC Bank Armenia",  IconURL = "https://rate.am/images/organization/logo/12.gif"},
                   new Bank {BankId = 14,Name = "AMIO BANK",          IconURL = "https://rate.am/images/organization/logo/b07ab5399fda4fea9c511ac0fa040288.png"},
                   new Bank {BankId = 15,Name = "Converse Bank",      IconURL = "https://rate.am/images/organization/logo/6988d8a8552c45eaaff4d5b779294d01.png"},
                   new Bank {BankId = 16,Name = "Ameriabank",         IconURL = "https://rate.am/images/organization/logo/da4585f3df0345778afb0a01e81203ea.png"},
                   new Bank {BankId = 17,Name = "Mellat Bank",        IconURL = "https://rate.am/images/organization/logo/17.gif"},
                   new Bank {BankId = 18,Name = "ARMECONOMBANK",      IconURL = "https://rate.am/images/organization/logo/e5ef9988870b4896be0399d803cedf57.jpg"}
           };

        var curs = new Currency[]{

              new Currency() { CurrencyId = 1, Name = "USD",IconURL = "https://rate.am/images/currency/icon/USD.gif", Symbol = "$" },
              new Currency() { CurrencyId = 2, Name = "EUR",IconURL = "https://rate.am/images/currency/icon/EUR.gif",Symbol = "€" },
              new Currency() { CurrencyId = 3, Name = "RUR",IconURL = "https://rate.am/images/currency/icon/RUR.gif", Symbol = "₽" },
              new Currency() { CurrencyId = 4, Name = "GBP",IconURL = "https://rate.am/images/currency/icon/GBP.gif", Symbol = "£" },
              new Currency() { CurrencyId = 5, Name = "GEL",IconURL = "https://rate.am/images/currency/icon/GEL.gif",Symbol = "₾" },
              new Currency() { CurrencyId = 6, Name = "CHF",IconURL = "https://rate.am/images/currency/icon/CHF.gif",Symbol = "₣" },
              new Currency() { CurrencyId = 7, Name = "CAD",IconURL = "https://rate.am/images/currency/icon/CAD.gif",Symbol = "$" },
              new Currency() { CurrencyId = 8, Name = "AED",IconURL = "https://rate.am/images/currency/icon/AED.gif",Symbol = "د.إ" },
              new Currency() { CurrencyId = 9, Name = "CNY",IconURL = "https://rate.am/images/currency/icon/CNY.gif",Symbol = "¥" },
              new Currency() { CurrencyId = 10,Name = "AUD",IconURL = "https://rate.am/images/currency/icon/AUD.gif", Symbol = "$" },
              new Currency() { CurrencyId = 11,Name = "JPY",IconURL = "https://rate.am/images/currency/icon/JPY.gif", Symbol = "¥" },
              new Currency() { CurrencyId = 12,Name = "SEK",IconURL = "https://rate.am/images/currency/icon/SEK.gif", Symbol = "kr" },
              new Currency() { CurrencyId = 13,Name = "HKD",IconURL = "https://rate.am/images/currency/icon/HKD.gif", Symbol = "$" },
              new Currency() { CurrencyId = 14,Name = "KZT",IconURL = "https://rate.am/images/currency/icon/KZT.gif", Symbol = "₸" },
              new Currency() { CurrencyId = 15,Name = "XAU",IconURL = "https://rate.am/images/currency/icon/XAU.gif", Symbol = "" },
           };

        serviceBank.SaveAll(banks).GetAwaiter();
        currencyService.SaveAllAsync(curs).GetAwaiter();

    }
}