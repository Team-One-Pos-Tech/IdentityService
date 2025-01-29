using IdentityService.Application.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.Models;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class EmailController(INotifyUserUseCase notifyUserUseCase) : ControllerBase
{
    [HttpPost("notify")]
    public async Task<IActionResult> NotifyUser([FromBody] NotifyOrderUpdateStatusRequest request)
    {
        await notifyUserUseCase.NotifyOrderUpdateStatus(request);

        return Ok("Notification sent!");
    }
}
