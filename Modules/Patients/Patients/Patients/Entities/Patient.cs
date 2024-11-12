
using Patients.DomainEvents;
using Shared.DDD;

namespace Patients.Patients.Entities
{
    public class Patient : Aggregate<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Gender { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        //Allergies

        private static List<Allergy> _allergies;
        public IReadOnlyCollection<Allergy> Allergies => _allergies.AsReadOnly();

        //MediaclRecords
        public ICollection<MedicalRecord> MedicalRecords { get; private set; }

        public static Patient Create (string firstName, string lastName, DateTime dateOfBirth, string gender, string email, string phoneNumber)
        {
            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(firstName);
            ArgumentException.ThrowIfNullOrEmpty(lastName);
            ArgumentException.ThrowIfNullOrEmpty(email);
            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Email = email,
                PhoneNumber = phoneNumber,

            };
            patient.AddDomainEvent(new PatientAddedToApplicationUsersDomainEvent(patient.Email));

            return patient;
        }


        public void  Update(string firstName, string lastName, DateTime dateOfBirth, string gender, string email, string phoneNumber)
        {
            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(firstName);
            ArgumentException.ThrowIfNullOrEmpty(lastName);
            ArgumentException.ThrowIfNullOrEmpty(email);

            
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static void AddAllergy(string allergyName, string allergyType, string reaction, DateTime dateReported)
        {
            var record = _allergies.FirstOrDefault(r => r.AllergyName.ToLower() == allergyName.ToLower());
            if (record == null)
            {
                var allergy = new Allergy(allergyName, allergyType, reaction, dateReported);
                _allergies.Add(allergy);
            }
        }

        public static void RemoveAllergy(Allergy allergy)
        {
            if (_allergies.Contains(allergy))
            {
                _allergies.Remove(allergy);
            }
        }


        public  void AddMedicalRecord(string title, string description, RecordType recordType)
        {
            var record = MedicalRecord.Create(this.Id, title, description,recordType);
            MedicalRecords.Add(record);

        }

        public void RemoveMedicalRecord(Guid recordId)
        {
            var record = MedicalRecords.FirstOrDefault(r => r.Id == recordId);
            if (record != null)
            {
                MedicalRecords.Remove(record);
            }
        }

    }
}
