using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.DTOs.User;
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
        public async Task<IActionResult> Authenticate([FromBody] UserCreateDto user)
        {
            var response = await _userRepository.Authenticate(user.Username, user.Password);
            if (response == null)
                return BadRequest(new { response = "Username or Password is invalid" });
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto user)
        {
            var isUserUnique = await _userRepository.UserIsUnique(user.Username);
            if (!isUserUnique)
            {
                return BadRequest(new { response = "User already exists" });
            }
            var newUser = await _userRepository.Register(user.Username, user.Password);
            if (newUser == null)
            {
                return BadRequest(new { response = "User not created due to a problem" });
            }
            return Ok(newUser);
        }
    }
}
