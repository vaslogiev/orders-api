using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    [HttpGet]
    public IActionResult GetStatus()
    {
        return Ok(new 
        { 
            Status = "Online", 
            Message = "Orders API е успешно пуснато в AWS EC2!", 
            Timestamp = DateTime.UtcNow 
        });
    }
}