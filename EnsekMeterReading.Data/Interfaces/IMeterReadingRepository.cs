using EnsekMeterReading.Data.Models;

namespace EnsekMeterReading.Data.Interfaces
{
    public interface IMeterReadingRepository
    {
        Task<Account?> GetAccount(int accountId);

        Task<MeterReading?> GetMeterReading(int accountId, DateTime meterReadingDateTime);

        Task AddMeterReading(MeterReading meterReading);
    }
}
