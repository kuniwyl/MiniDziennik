using Dziennik.Server.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dziennik.Server.Controllers
{
    [ApiController()]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest) 
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userService);
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);
            if (userSession == null)
            {
                return Unauthorized();
            }
            else return userSession;
        }

        [HttpPost("regiter")]
        [AllowAnonymous]
        public ActionResult<UserSession> Register([FromBody] UserRequest userRequest)
        {
            var user = _userService.PostUser(userRequest);
            Console.WriteLine(user);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost("checkToken")]
        [Authorize]
        public ActionResult<bool> CheckToken(string token)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userService);
            var user = jwtAuthenticationManager.GetClaimsPrincipalFromToken(token);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
