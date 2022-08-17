using EnsekMeterReading.Data.Interfaces;
using EnsekMeterReading.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsekMeterReading.Data
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly IMeterReadingContext context;

        public MeterReadingRepository(IMeterReadingContext context)
        {
            this.context = context;
        }

        public async Task AddMeterReadings(List<MeterReading> meterReadings)
        {
            this.context.MeterReadings.AddRange(meterReadings);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> AccountExists(int accountId)
        {
            return await this.context.Accounts.AnyAsync(a => a.AccountId == accountId);
        }

        public async Task<bool> MeterReadingExists(int accountId, DateTime meterReadingDateTime)
        {
            return await this.context.MeterReadings.AnyAsync(r => r.AccountId == accountId && r.MeterReadingDateTime == meterReadingDateTime);
        }
    }
}
