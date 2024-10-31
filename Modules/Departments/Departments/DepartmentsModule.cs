using Departments.Data.Seed;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Departments
{
    public  static class DepartmentsModule
    {
        public static IServiceCollection AddDepartmentsModule(this IServiceCollection services, IConfiguration configuration)
        {

           
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

            services.AddDbContext<DepartmentsDbContext>((sp,options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

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
