using Microsoft.EntityFrameworkCore;
using EnsekMeterReading.Data.Models;
using EnsekMeterReading.Data.Interfaces;

namespace EnsekMeterReading.Data
{
    public class MeterReadingContext : DbContext, IMeterReadingContext
    {
        public MeterReadingContext(DbContextOptions<MeterReadingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new SeedDatabase(modelBuilder).Initialize();
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<MeterReading> MeterReadings { get; set; }

        public Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
