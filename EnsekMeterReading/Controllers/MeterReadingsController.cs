using EnsekMeterReading.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnsekMeterReading.Controllers
{
    [ApiController]
    [Route("meter-readings-uploads")]
    public class MeterReadingsController : ControllerBase
    {
        private readonly ILogger<MeterReadingsController> _logger;
        private readonly IMediator mediator;

        public MeterReadingsController(ILogger<MeterReadingsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task UploadMeterReadings(IFormFile file)
        {
            if (file.ContentType != "text/csv" || !file.FileName.EndsWith(".csv"))
            {
                throw new ArgumentException("File type must be CSV");
            }

            var request = new MeterReadingFileRequest { MeterReadingFile = file };
            await this.mediator.Send(request);
        }
    }
}