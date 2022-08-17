using EnsekMeterReading.Data.Interfaces;
using EnsekMeterReading.Data.Models;
using EnsekMeterReading.Services.Interfaces;
using EnsekMeterReading.Services.Models;
using System.Globalization;

namespace EnsekMeterReading.Services
{
    public class MeterReadingFileHandler : IMeterReadingFileHandler
    {
        private readonly IMeterReadingRepository meterReadingRepository;

        private MeterReadingResponse meterReadingResponse = new();

        public MeterReadingFileHandler(IMeterReadingRepository meterReadingRepository)
        {
            this.meterReadingRepository = meterReadingRepository;
        }

        public async Task<MeterReadingResponse> Handle(MeterReadingFileRequest request, CancellationToken cancellationToken)
        {
            this.ParseFile(request);

            if (meterReadingResponse.Errors.Any())
            {
                return meterReadingResponse;
            }


            return meterReadingResponse;
        }

        private List<MeterReading> ParseFile(MeterReadingFileRequest request)
        {
            var meterReadings = new List<MeterReading>();
            using (var stream = new StreamReader(request.MeterReadingFile.OpenReadStream()))
            {
                if (stream.Peek() < 0)
                {
                    meterReadingResponse.Errors.Add("Meter Reading File is empty");
                    return meterReadings;
                }

                var firstLine = stream.ReadLine();
                if (firstLine != "AccountId,MeterReadingDateTime,MeterReadValue")
                {
                    meterReadingResponse.Errors.Add("Meter Reading File is in incorrect format");
                    return meterReadings;
                }

                while (stream.Peek() >= 0)
                {
                    var meterReadingEntry = stream.ReadLine().Split(",");
                    if (this.validateEntry(meterReadingEntry))
                    {
                        meterReadings.Add(new MeterReading
                        {
                            AccountId = int.Parse(meterReadingEntry[0]),
                            MeterReadingDateTime = DateTime.ParseExact(meterReadingEntry[1], "d/M/yyyy HH:mm", CultureInfo.CurrentCulture),
                            MeterReadValue = long.Parse(meterReadingEntry[2]),
                        });
                    }
                }
            }

            return meterReadings;
        }

        private bool validateEntry(string[] meterReadingEntry)
        {
            if (!int.TryParse(meterReadingEntry[0], out var accountId)) {
                this.meterReadingResponse.Errors.Add($"Failed to convert account id {meterReadingEntry[0]}");
                return false;
            }

            if (!DateTime.TryParseExact(meterReadingEntry[1], "d/M/yyyy HH:mm", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out var dateTime)) {
                this.meterReadingResponse.Errors.Add($"Failed to convert meter reading date for account id {meterReadingEntry[0]}. Value provided was {meterReadingEntry[1]}");
                return false;
            }

            if (!long.TryParse(meterReadingEntry[2], out var meterRedingValue)) {
                this.meterReadingResponse.Errors.Add($"Failed to convert meter reading value for account id {meterReadingEntry[0]}. Value provided was {meterReadingEntry[2]}");
                return false;
            }

            return true;
        }
    }
}