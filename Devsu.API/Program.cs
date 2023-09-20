using DataAccessLayer.Data;
using Devsu.Domain.Interfaces;
using Devsu.Service;
using Devsu.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Devsu.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.WriteTo.Console()
.WriteTo.File("logs/devsu-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog(logger);
builder.Logging.AddConsole();


// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
builder.Services.AddDbContext<DevsuContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<Func<DevsuContext>>((provider) => () => provider.GetService<DevsuContext>());
builder.Services.AddScoped<DbFactory>();
builder.Services.AddScoped<IWorkUnit, WorkUnit>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMovementService, MovementService>();
builder.Services.AddControllers();

builder.Services.AddOptions();
var app = builder.Build();

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
Log.CloseAndFlush();
