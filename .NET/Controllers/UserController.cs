using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
      //  [Authorize(Roles = "Admin")]
        [Route("admin")]
        public IActionResult AdminEndpoint()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
             if (username == null || userId == null)
            {
                return Unauthorized("Invalid token");
            }
           return Ok(new { message = "Hello Admin", username, userId });
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("user")]
        public IActionResult UserEndpoint()
        {
             var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
             if (username == null || userId == null)
            {
                return Unauthorized("Invalid token");
            }
           return Ok(new { message = "Hello User", username, userId });
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IActionResult AllEndpoint()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
             if (username == null || userId == null)
            {
                return Unauthorized("Invalid token");
            }
           return Ok(new { message = "Hello all", username, userId });
        }
    }
}
