using Shared.Data.Seed;

namespace Doctors.Data.Seed
{
    public class DoctorsSeed(DoctorsDbContext context) : ISeedData
    {
        public async Task SeedAsync()
        {

            if (!context.Doctors.Any())
            {
                var entities = new List<Doctor>();
                foreach (var item in InitialData.Doctors)
                {
                    entities.Add(Doctor.Create(item));
                }
                
                await context.Doctors.AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }

        }
    }
}
