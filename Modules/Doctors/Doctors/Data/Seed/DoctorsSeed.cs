
using Departments.Contracts.Departments.Features.GetDepartments;
using Shared.Data.Seed;

namespace Doctors.Data.Seed
{
    public class DoctorsSeed(DoctorsDbContext context, ISender sender) : ISeedData
    {
        public async Task SeedAsync()
        {
            if (!context.Specializations.Any())
            {
               
                await context.Specializations.AddRangeAsync(InitialData.Specializations);
                await context.SaveChangesAsync();
            }
            if (!context.Doctors.Any())
            {
                var departments = await sender.Send(new GetDepartmentsQuery());
               


                var specializationIds = context.Specializations.ToDictionary(s => s.Name, s => s.Id);
                var departmentsIds= departments.DepartmentDtos.ToDictionary(s=>s.Name, s => s.Id);

                var entities = new List<Doctor>();

                foreach (var item in InitialData.Doctors)
                {

                    var departmentId = departmentsIds.FirstOrDefault(s => s.Key == item.Department).Value;
                    if(departmentId == Guid.Empty)
                    {
                        continue;
                    }
                    var specializationId = specializationIds.FirstOrDefault(s => s.Key == item.Specialization).Value;
                     if (specializationId == Guid.Empty)
                    {
                        continue;
                    }
                    entities.Add(Doctor.Create(
                       name: item.Name,
                       surname: item.Surname,
                       departmentId: departmentId,
                       specializationId: specializationId,
                       workingStartDate: item.WorkingStartDate,
                       graduatedUniversity: item.GraduatedUniversity,
                       email: "testc@gmail.com"
                     ));

                    
                }

                await context.Doctors.AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }

        }
    }
}
