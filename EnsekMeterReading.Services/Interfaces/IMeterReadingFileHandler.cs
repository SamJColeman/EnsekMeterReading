using EnsekMeterReading.Services.Models;
using MediatR;

namespace EnsekMeterReading.Services.Interfaces
{
    public interface IMeterReadingFileHandler : IRequestHandler<MeterReadingFileRequest, MeterReadingResponse>
    {
    }
}
