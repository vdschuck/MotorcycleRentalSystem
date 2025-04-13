using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Application.Rent;

namespace MotorcycleRentalSystem.Controllers;

[Route("locacao")]
[ApiController]
public class RentController(IRentService rentService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await rentService.ConsultRentAsync(id);
        return result is null ? NotFound("Locação não encontrada") : Created();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RentMotorcycleRequest data)
    {
        var (rent, moto) = await rentService.MotorcycleRentalProcessAsync(data);

        if (rent == 1 && moto == 1) return Ok();

        return BadRequest("Dados inválidos");
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> Put(string id, [FromBody] DevolveMotorcycleRequest data)
    {
        var (rent, moto) = await rentService.MotorcycleDevolveProcessAsync(id, data);

        if (rent == 1 && moto == 1) return Ok("Data de devolução informada com sucesso");

        return BadRequest("Dados inválidos");
    }
}