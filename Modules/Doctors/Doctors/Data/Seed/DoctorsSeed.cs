using Doctors.Doctors.Dtos;
using Shared.Data.Seed;

namespace Doctors.Data.Seed
{
    public class DoctorsSeed(DoctorsDbContext context) : ISeedData
    {
        public async Task SeedAsync()
        {
            if (!context.Specializations.Any())
            {
                var specializationEntities = new List<Specialization>();
                foreach(var specializationEntity in InitialData.Specializations)
                {
                    specializationEntities.Add(Specialization.CreateSpecialization(specializationEntity.Name, specializationEntity.Description));
                }
                await context.Specializations.AddRangeAsync(specializationEntities);
                await context.SaveChangesAsync();
            }
            if (!context.Doctors.Any())
            {
                var specializationIds = context.Specializations.ToDictionary(s => s.Name, s => s.Id);
                var entities = new List<Doctor>();
                foreach (var item in InitialData.Doctors)
                {
                    var specializationId = specializationIds.GetValueOrDefault(item.Specialization);
                    entities.Add(Doctor.Create(item, specializationId));
                }
                
                await context.Doctors.AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }

        }
    }
}
