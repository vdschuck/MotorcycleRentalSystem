using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Application.DeliveryMan;
using MotorcycleRentalSystem.Application.Motorcycle;
using MotorcycleRentalSystem.Application.Rent;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Extensions;
using MotorcycleRentalSystem.Infrastructure;
using MotorcycleRentalSystem.Infrastructure.Repositories;
using MotorcycleRentalSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

// Services
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IDeliveryManService, DeliveryManService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<PostgreDbContext>();

app.Run();