using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Application.Motorcycle;

namespace MotorcycleRentalSystem.Controllers;

[Route("motos")]
[ApiController]
public class MotorcycleController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] CreateMotorcycleRequest data)
    {
    }

    [HttpPut("{id}/placa")]
    public void Put(int id, [FromBody] UpdateMotorcycleRequest data)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}