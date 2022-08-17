using Microsoft.EntityFrameworkCore;
using EnsekMeterReading.Data.Models;

namespace EnsekMeterReading.Data
{
    public class SeedDatabase
    {
        private readonly ModelBuilder modelBuilder;

        public SeedDatabase(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Initialize()
        {
            try
            {
                var accountRows = File.ReadAllLines(Environment.CurrentDirectory + "Test_accounts.csv");

                accountRows.Skip(1).ToList().ForEach(r =>
                {
                    var values = r.Split(",");
                    this.modelBuilder.Entity<Account>().HasData(new Account
                    {
                        AccountId = int.Parse(values[0]),
                        FirstName = values[1],
                        LastName = values[2]
                    });
                });
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("Failed to open test account data, please ensure it is in the correct location (see readme.md in github");
            }
        }
    }
}
