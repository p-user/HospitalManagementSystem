using Shared.DDD;

namespace Departments.Departments.Models
{
    public class Department : Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public Guid HeadOfDepartment { get; private set; } = default!;
        public List<Guid> Doctors { get; private set; }



        //add working shifts 


        public static Department Create(string name, string description, Guid headOfDepartment)
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
                HeadOfDepartment = headOfDepartment,
                Doctors = new List<Guid>() { headOfDepartment }

               
            };

            return entity;
        }


        public void Update(string name, string description, Guid headOfDepartment)
        {

            //validate incoming request 
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(description);

            //Create

            Name = name;
            Description = description;
            HeadOfDepartment = headOfDepartment;

           
        }

    }
}
