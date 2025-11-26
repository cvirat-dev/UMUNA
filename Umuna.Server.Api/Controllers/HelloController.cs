using Microsoft.AspNetCore.Mvc;

namespace Umuna.Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from Umuna WebAPI!");
        }
    }
}
