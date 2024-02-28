using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RateAmData.Entities;

namespace RateAmData
{
    public class RateAmDataContext : DbContext
    {
        public DbSet<CurrencyEntity> Currencies { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<RateEntity> Rates { get; set; }
        public DbSet<LastDateEntity> LastDates { get; set; }


        protected readonly IConfiguration Configuration;

        public RateAmDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                Configuration.GetConnectionString("RateDb"))
                .EnableSensitiveDataLogging()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


        }

    }
}
