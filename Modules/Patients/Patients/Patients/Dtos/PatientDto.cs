

using System.ComponentModel.DataAnnotations;

namespace Patients.Patients.Dtos
{
    public record  PatientDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; init; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; init; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; init; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; init; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; init; } = string.Empty;
    }
}
