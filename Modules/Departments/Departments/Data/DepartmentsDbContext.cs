﻿using Departments.Departments.Models;

namespace Departments.Data
{
    public class DepartmentsDbContext : DbContext
    {
        public DepartmentsDbContext(DbContextOptions<DepartmentsDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Departments");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
            base.OnModelCreating(builder);
        }
    }
}
