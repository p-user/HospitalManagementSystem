
using Shared;

namespace Departments.Departments.Models
{
    public class Department : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;


        //add working shifts 


        public static Department Create(string name, string description)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);
          
            //Create
            var entity = new Department
            {

                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
               
            };

            return entity;
        }


        public void Update(string name, string description)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);

            //Create

            Name = name;
            Description = description;

           
        }
    }
}
