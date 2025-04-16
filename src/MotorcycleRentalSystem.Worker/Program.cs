using Microsoft.EntityFrameworkCore;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Domain.Services;
using MotorcycleRentalSystem.Infrastructure;
using MotorcycleRentalSystem.Infrastructure.Configuration;
using MotorcycleRentalSystem.Infrastructure.Repositories;
using MotorcycleRentalSystem.Infrastructure.Services.AWS;
using MotorcycleRentalSystem.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection")));

builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMotorcycleEventRepository, MotorcycleEventRepository>();
builder.Services.AddScoped<ISimpleQueueService, SimpleQueueService>();
builder.Services.Configure<AWSOptions>(builder.Configuration.GetSection("AWS"));

var host = builder.Build();
host.Run();