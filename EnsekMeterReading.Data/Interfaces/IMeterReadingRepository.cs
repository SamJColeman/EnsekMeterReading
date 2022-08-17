using EnsekMeterReading.Data.Models;

namespace EnsekMeterReading.Data.Interfaces
{
    public interface IMeterReadingRepository
    {
        Task<bool> AccountExists(int accountId);

        Task<bool> MeterReadingExists(int accountId, DateTime meterReadingDateTime);

        Task AddMeterReading(MeterReading meterReading);
    }
}
