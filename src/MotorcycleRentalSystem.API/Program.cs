using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Application.Motorcycle;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Infrastructure;
using MotorcycleRentalSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

// Repositories
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

// Services
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();