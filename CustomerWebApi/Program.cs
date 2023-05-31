using CustomerWebApi.Data;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Cấu hình DB context DI

var dbHost = Environment.GetEnvironmentVariable("DB_HOST"); 
var dbname = Environment.GetEnvironmentVariable("DB_NAME"); 
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectString = $"Data Source={dbHost}; Initial Catalog = {dbname};User ID = sa; Password = {dbPass} ";
builder.Services.AddDbContext<CustomerDbContext>(otp => otp.UseSqlServer(connectString));

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
