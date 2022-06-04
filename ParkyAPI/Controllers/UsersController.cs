using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Repository.Interfaces;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var response = await _userRepository.Authenticate(user.Name, user.Password);
            if (response == null)
                return BadRequest(new { response = "Username or Password is invalid" });
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var isUserUnique = await _userRepository.UserIsUnique(user.Name);
            if (!isUserUnique)
            {
                return BadRequest(new { response = "User already exists" });
            }
            var newUser = await _userRepository.Register(user.Name, user.Password);
            if (newUser == null)
            {
                return BadRequest(new { response = "User not created due to a problem" });
            }
            return Ok(newUser);
        }
    }
}
