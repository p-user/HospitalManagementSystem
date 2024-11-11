

using Authentication.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Authentication.Dtos
{
    public record ApplicationUserDto
    {
        [Required, EmailAddress]
        public string Email;

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string? Password;


        public string? UserName;

        [Required]
        public required string Role { get; init; }
    }
}
