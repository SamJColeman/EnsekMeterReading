﻿using EnsekMeterReading.Data.Interfaces;
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

        public async Task AddMeterReading(MeterReading meterReading)
        {
            this.context.MeterReadings.Add(meterReading);
            await this.context.SaveChangesAsync();
        }

        public async Task<Account?> GetAccount(int accountId)
        {
            return await this.context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<MeterReading?> GetMeterReading(int accountId, DateTime meterReadingDateTime)
        {
            return await this.context.MeterReadings.FirstOrDefaultAsync(r => r.AccountId == accountId && r.MeterReadingDateTime == meterReadingDateTime);
        }
    }
}