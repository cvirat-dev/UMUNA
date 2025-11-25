using Microsoft.AspNetCore.Mvc;

namespace Umuna.ApiServer.Controllers
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
