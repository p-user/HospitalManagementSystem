
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

    }
}
