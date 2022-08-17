using MediatR;
using Microsoft.AspNetCore.Http;

namespace EnsekMeterReading.Services.Models
{
    public class MeterReadingFileRequest : IRequest<MeterReadingResponse>
    {
        public IFormFile MeterReadingFile { get; set; }
    }
}
