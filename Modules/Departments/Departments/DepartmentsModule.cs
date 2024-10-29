

namespace Departments
{
    public  static class DepartmentsModule
    {
        public static IServiceCollection AddDepartmentsModule(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DepartmentsDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableInterceptor());

            });

           // services.AddScoped<ISeedData, DoctorsSeed>();
            return services;
        }

        public static IApplicationBuilder UseDepartmentsModule(this IApplicationBuilder app)
        {
            app.UseMigration<DepartmentsDbContext>();


            return app;
        }

    }
}
