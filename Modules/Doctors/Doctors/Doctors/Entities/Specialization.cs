
using Shared;

namespace Doctors.Doctors.Entities
{
    public class Specialization : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; }

        public static Specialization CreateSpecialization(string name, string description)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            var entity = new Specialization
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,

            };
            return entity;
           
        }

        public void Update(Guid id, string name, string description)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);

            Name = name;
            Description =description;
        }
    }
}
