using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorkshopAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<bdeContext>(options => options.UseMySql(connectionString ?? throw new InvalidOperationException(), ServerVersion.AutoDetect(connectionString)));

Console.WriteLine("BDD connected !");

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("API is ready !");

app.Run();
