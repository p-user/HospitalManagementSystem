using Shared.Data.Seed;

namespace Departments.Data.Seed
{
    public class DepartmentsSeed(DepartmentsDbContext departmentsDbContext) : ISeedData
    {
        public async Task SeedAsync()
        {
            if (!departmentsDbContext.Departments.Any())
            { 
                departmentsDbContext.Departments.AddRange(InitialData.Departments);
                await departmentsDbContext.SaveChangesAsync();
            }
        }
    }
}
