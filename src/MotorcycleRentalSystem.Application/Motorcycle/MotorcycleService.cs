using System.Text.Json;
using Microsoft.Extensions.Logging;
using MotorcycleRentalSystem.Domain.Repositories;
using MotorcycleRentalSystem.Domain.Services;

namespace MotorcycleRentalSystem.Application.Motorcycle;

public class MotorcycleService(
    IMotorcycleRepository repository,
    ISimpleQueueService sqs,
    ILogger<MotorcycleService> logger) : IMotorcycleService
{
    public async Task<int> RegisterNewMotorcycleAsync(CreateMotorcycleRequest data)
    {
        logger.LogInformation("Register new motorcycle with {Plate}", data.Plate);
        var newMoto = Domain.Entities.Motorcycle.CreateNewMotorcycle(data.Id, data.Year, data.Plate, data.Model);
        var result = await repository.AddAsync(newMoto);
        var newEvent = GetMotorcycleResponse.FromEntity(newMoto);
        await sqs.SendMessageAsync(JsonSerializer.Serialize(newEvent));
        return result;
    }

    public async Task<GetMotorcycleResponse?> GetMotorcycleByIdAsync(string id)
    {
        logger.LogInformation("Get motorcycle with id {id}", id);
        var result = await repository.GetByIdAsync(id);
        logger.LogInformation(result == null
            ? "Motorcycle with Id {id} not found"
            : "Motorcycle with Id {id} found", id);

        return result is null ? null : GetMotorcycleResponse.FromEntity(result);
    }

    public async Task<List<GetMotorcycleResponse>> GetAllMotorcyclesAsync(SearchMotorcycleRequest? data)
    {
        logger.LogInformation("Get a list of motorcycles");
        var result = await repository.FindAllAsync(data?.Plate);
        return result.Select(GetMotorcycleResponse.FromEntity).ToList();
    }

    public Task<int> DeleteMotorcycleAsync(string id)
    {
        logger.LogInformation("Delete an motorcycle by {id}", id);
        return repository.DeleteByIdAsync(id);
    }

    public async Task<int> UpdatePlateNumberAsync(string id, UpdateMotorcycleRequest data)
    {
        logger.LogInformation("Update motorcycle plate with {id}", id);
        var motorcycle = await repository.GetByIdAsync(id);
        if (motorcycle == null) return 0;

        motorcycle.UpdatePlate(data.Plate);
        return await repository.UpdateAsync(motorcycle);
    }
}