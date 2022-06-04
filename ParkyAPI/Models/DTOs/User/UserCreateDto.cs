using System.ComponentModel.DataAnnotations;

namespace ParkyAPI.Models.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
