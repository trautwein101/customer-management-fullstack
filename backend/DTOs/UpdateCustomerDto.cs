using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Api.DTOs
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
