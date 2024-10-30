using Departments.Data.Seed;
using Shared.Data.Seed;

namespace Departments
{
    public  static class DepartmentsModule
    {
        public static IServiceCollection AddDepartmentsModule(this IServiceCollection services, IConfiguration configuration)
        {

           
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DepartmentsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableInterceptor());

            });

            services.AddScoped<ISeedData, DepartmentsSeed>();
            return services;
        }

        public static IApplicationBuilder UseDepartmentsModule(this IApplicationBuilder app)
        {
            app.UseMigration<DepartmentsDbContext>();


            return app;
        }

    }
}
