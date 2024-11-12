
using System.ComponentModel.DataAnnotations;

namespace Patients.Patients.Dtos
{
    public record MedicalRecordDto
    {
        [Required(ErrorMessage = "Patient ID is required.")]
        public Guid PatientId { get; init; }

        [Required(ErrorMessage = "Record title is required.")]
        [StringLength(100, ErrorMessage = "Record title cannot be of more than 100 characters.")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Details are required.")]
        [StringLength(1000, ErrorMessage = "Details cannot be of more than  1000 characters.")]
        public string Description { get; init; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Record Type is required.")]
        [EnumDataType(typeof(RecordType), ErrorMessage = "Invalid Record Type.")]
        public RecordType RecordType { get; init; }
    }

    public enum RecordType
    {
        Diagnosis = 1,
        Prescription = 2,
        Procedure = 3,
        LabResult = 4,
        Consultation = 5
    }
}
