using EnsekMeterReading.Data;
using EnsekMeterReading.Data.Interfaces;
using EnsekMeterReading.Services;
using EnsekMeterReading.Services.Interfaces;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IMeterReadingFileHandler).Assembly);
builder.Services.AddTransient<IMeterReadingFileHandler, MeterReadingFileHandler>();
builder.Services.AddTransient<IMeterReadingRepository, MeterReadingRepository>();
builder.Services.AddTransient<IMeterReadingContext, MeterReadingContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MeterReadingContext>(options => options.UseSqlite(new SqliteConnection("DataSource=file::memory:?cache=shared")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MeterReadingContext>();
    dbContext.Database.EnsureCreated();
    SeedDatabase.Initialize(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
