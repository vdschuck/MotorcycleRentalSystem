using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Application.DeliveryMan;

namespace MotorcycleRentalSystem.Controllers;

[Route("entregadores")]
[ApiController]
public class DeliveryManController(IDeliveryManService deliveryManService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDeliveryManRequest data)
    {
        var result = await deliveryManService.RegisterDeliveryMan(data);
        return result == 1 ? Created() : BadRequest("Dados inválidos");
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> Put(string id, [FromBody] UploadDriverLicensePhotoRequest data)
    {
        var result = await deliveryManService.UploadDriverLicensePhoto(id, data);
        return result == 1 ? Created() : BadRequest("Dados inválidos");
    }
}