

using System.ComponentModel.DataAnnotations;

namespace Patients.Patients.Dtos
{
    public record  AllergyDto
    {
        [Required]
        [MaxLength(100)]
        public string AllergyName { get; init; }
        [Required]
        [MaxLength(100)]
        public string AllergyType { get; init; }
        [Required]
        [MaxLength(100)]
        public string Reaction { get; init; }
    }
}
