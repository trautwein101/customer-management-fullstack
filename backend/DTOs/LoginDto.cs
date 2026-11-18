using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Api.DTOs
{
    public class LoginDto
    {

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
