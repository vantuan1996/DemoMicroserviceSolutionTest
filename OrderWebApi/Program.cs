using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using Common;
using OrderWebApi.Datas;
using OrderWebApi.Models;
using OrderWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Cấu hình DB context DI

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbname = Environment.GetEnvironmentVariable("DB_NAME");
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var connectString = $"Data Source={dbHost}; Initial Catalog = {dbname};User ID = sa; Password = {dbPass} ";
//var connectString = $"Data Source={"DESKTOP-GGDOF1I"}; Initial Catalog = {"dbOrder"};User ID = sa; Password = {"123456"} ";
var connectString = AppSettingHelper.GetStringFromFileJson("appsettings", "ConnectionStrings:DefaultConnection").Result;
builder.Services.AddDbContext<OrderDbContext>(otp => otp.UseSqlServer(connectString));
builder.Services.AddConsulConfig(builder.Configuration);
//builder.Services.AddDbContext<OrderDetailDbContext>(otp => otp.UseSqlServer(connectString));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
app.UseConsul(builder.Configuration);
app.Run();
