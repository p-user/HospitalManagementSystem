
using Shared.DDD;

namespace Patients.Patients.Entities
{
    public class Allergy 
    {
        public string AllergyName { get; private set; }
        public string AllergyType { get; private set; } 
        public string Reaction { get; private set; }
        public DateTime DateReported { get; private set; }


        public Allergy(string allergyName, string allergyType, string reaction, DateTime dateReported)
        {
            if (string.IsNullOrEmpty(allergyName)) throw new ArgumentException("Allergy name cannot be empty.");
            if (string.IsNullOrEmpty(allergyType)) throw new ArgumentException("Allergy type cannot be empty.");
            if (string.IsNullOrEmpty(reaction)) throw new ArgumentException("Reaction cannot be empty.");

            AllergyName = allergyName;
            AllergyType = allergyType;
            Reaction = reaction;
            DateReported = dateReported;
        }
    }
}
