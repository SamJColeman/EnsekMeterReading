using Microsoft.EntityFrameworkCore;
using EnsekMeterReading.Data.Models;

namespace EnsekMeterReading.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(MeterReadingContext context)
        {
            try
            {
                var accountRows = File.ReadAllLines(Environment.CurrentDirectory + "\\Test_accounts.csv");

                accountRows.Skip(1).ToList().ForEach(r =>
                {
                    var values = r.Split(",");
                    var account = new Account
                    {
                        AccountId = int.Parse(values[0]),
                        FirstName = values[1],
                        LastName = values[2]
                    };
                    if (!context.Accounts.Contains(account))
                    {
                        context.Accounts.Add(account);
                    }
                });
                context.SaveChanges();
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Failed to open test account data, please ensure it is in the correct location (see readme.md in github");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
