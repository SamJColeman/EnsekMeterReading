namespace EnsekMeterReading.Services.Models
{
    public class MeterReadingResponse
    {
        public int ReadingsAddedSuccessfully = 0;
        public List<string> Errors { get; set; } = new();
    }
}
