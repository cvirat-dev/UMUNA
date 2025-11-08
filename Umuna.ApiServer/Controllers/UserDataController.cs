using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.DTOs;
using Umuna.ApiServer.Services;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDataDto>> GetAllUsers()
        {
            // This method should return all users from the user store.
            // For now, we will return a static list of users.
            var users = UserStore.Users;
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            return Ok(users);
        }
    }
}
