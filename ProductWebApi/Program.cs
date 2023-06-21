using Microsoft.EntityFrameworkCore;
using ProductWebApi;
using ProductWebApi.Data;
using System.Xml.Linq;
using Common;
var builder = WebApplication.CreateBuilder(args);
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
var connectString = $"server={"localhost"};port=3306;database={"dbProduct"};user=root;password={"123456"}";
//var connectString = $"Data Source={dbHost}; Initial Catalog = {dbName};User ID = sa; Password = {dbPassword} ";
//var connectString = AppSettingHelper.GetStringFromFileJson("appsettings", "ConnectionStrings:DefaultConnection").Result;
builder.Services.AddDbContext<ProductDbContext>(o => o.UseMySQL(connectString));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton<FakeDataStore>();
builder.Services.AddConsulConfig(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseConsul(builder.Configuration);
app.MapControllers();

app.Run();
