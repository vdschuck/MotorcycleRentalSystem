using Microsoft.AspNetCore.Mvc;
using MotorcycleRentalSystem.Application.DeliveryMan;
using MotorcycleRentalSystem.Extensions;
using MotorcycleRentalSystem.Responses;

namespace MotorcycleRentalSystem.Controllers;

[Route("entregadores")]
[ApiController]
public class DeliveryManController(IDeliveryManService deliveryManService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDeliveryManRequest data)
    {
        var result = await deliveryManService.RegisterDeliveryMan(data);
        return result == 1 ? Created() : BadRequest(MessageResponse.From(AppMessage.InvalidData.GetMessage()));
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> UploadPhoto(string id, [FromBody] UploadDriverLicensePhotoRequest data)
    {
        await deliveryManService.UploadDriverLicensePhoto(id, data);
        return Created();
    }
}