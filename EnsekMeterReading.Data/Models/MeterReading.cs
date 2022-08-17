namespace EnsekMeterReading.Data.Models
{
    public class MeterReading
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public Int64 MeterReadValue { get; set; }
    }
}
