using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Application.Motorcycle;

namespace MotorcycleRentalSystem.Controllers;

[Route("motos")]
[ApiController]
public class MotorcycleController(IMotorcycleService motorcycleService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await motorcycleService.GetAllMotorcyclesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await motorcycleService.GetMotorcycleByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateMotorcycleRequest data)
    {
        await motorcycleService.RegisterNewMotorcycleAsync(data);
        return Created();
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateMotorcycleRequest data)
    {
        var result = await motorcycleService.UpdatePlateNumberAsync(id, data);
        return result == 1 ? Ok("Placa modificada com sucesso") : BadRequest("Dados inválidos");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await motorcycleService.DeleteMotorcycleAsync(id);
        return result == 1 ? Ok() : BadRequest("Dados inválidos");
    }
}