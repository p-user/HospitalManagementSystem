
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
            //if (!context.Doctors.Any())
            //{
            //    var specializationIds = context.Specializations.ToDictionary(s => s.Name, s => s.Id);
            //    var entities = new List<Doctor>();
            //    foreach (var item in InitialData.Doctors)
            //    {
            //        var department = "";//retrive departmentId using ISender + implement endpoint to retrieve departmentId
            //        Doctor.Create(
            //           Name: item.Name,
            //           Surname: item.Surname,
            //           DepartmentId: department,
            //           SpecializationId: context.Specializations.First(s => s.Name == item.Specialization).Id,
            //           WorkingStartDate: item.WorkingStartDate,
            //           GraduatedUniversity: item.GraduatedUniversity
            //         );
            //    }
                
            //    await context.Doctors.AddRangeAsync(entities);
            //    await context.SaveChangesAsync();
            //}

        }
    }
}
