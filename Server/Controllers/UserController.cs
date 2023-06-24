using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dziennik.Server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "HeadAdmin")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers(string type)
        {
            var result = _userService.GetUsers(type);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetUser(int id, string type)
        {
            Console.WriteLine(type);
            var result = _userService.GetUser(id, type);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> PostUser(UserRequest request)
        {
            var result = _userService.PostUser(request);
            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> PutUser(int id, UserRequest request)
        {
            var result = _userService.PutUser(id, request);
            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteUser(int id, string type)
        {
            var result = _userService.DeleteUser(id, type);
            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("shortList")]
        [Authorize]
        public async Task<ActionResult<List<ShortListResponse>>> GetShortList(string type)
        {
            var result = _userService.GetShortListResponse(type);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

    }
}
