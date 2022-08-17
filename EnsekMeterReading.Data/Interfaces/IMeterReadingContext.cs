using EnsekMeterReading.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsekMeterReading.Data.Interfaces
{
    public interface IMeterReadingContext
    {
        DbSet<Account> Accounts { get; }
        DbSet<MeterReading> MeterReadings { get; set; }

        Task SaveChangesAsync();
    }
}
