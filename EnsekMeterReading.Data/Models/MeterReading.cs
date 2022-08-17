using DataAnnotationsExtensions;

namespace EnsekMeterReading.Data.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        public int AccountId { get; set; }        
        public Account Account { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        [Max(999999)]
        public int MeterReadValue { get; set; }
    }
}
