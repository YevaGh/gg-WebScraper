using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace RateAmLib.Data
{
    internal class ScrapeDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ScrapeDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("RateDb"));
        }
    }
}
