using Assessment_Test_BLL.IService;
using Assessment_Test_DAL.Model.PostModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment_Test_API.Controllersl
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var token = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task <IActionResult> GetProfile()
        {
            var user = await _userService.GetProfile(User.Identity.Name, "");
            return Ok(user);
        }
    }
}
