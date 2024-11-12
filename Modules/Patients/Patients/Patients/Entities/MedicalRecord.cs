
using Shared.DDD;

namespace Patients.Patients.Entities
{
    public class MedicalRecord : Entity<Guid>
    {
        public Guid PatientId { get; private set; }  
        public Patient Patient { get; private set; }
        public string RecordTitle { get; private set; }  
        public string Details { get; private set; }  
        public DateTime Date { get; private set; }
        public RecordType? RecordType { get; set; }


        public static MedicalRecord Create(Guid patientId, string title, string description, RecordType recordType)
        {
            ArgumentException.ThrowIfNullOrEmpty(title);
            ArgumentException.ThrowIfNullOrEmpty(description);

            return new MedicalRecord
            {
                PatientId = patientId,
                RecordTitle = title,
                Details = description,
                Date = DateTime.UtcNow,
                RecordType = recordType

            };
        }


        public void UpdateRecord(string title, string description, RecordType recordType)
        {
            ArgumentException.ThrowIfNullOrEmpty(title);
            ArgumentException.ThrowIfNullOrEmpty(description);

            RecordTitle = title;
            Details = description;
            RecordType = recordType;
            
        }
    }
}
