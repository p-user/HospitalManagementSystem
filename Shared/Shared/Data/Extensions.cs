using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Seed;

namespace Shared.Data
{
    public static class Extensions
    {

        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder applicationBuilder) where TContext : DbContext
        {

            MigrateDbAsync<TContext>(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();
            SeedData(applicationBuilder.ApplicationServices).GetAwaiter().GetResult();
            return applicationBuilder;
        }

        private static async Task MigrateDbAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<TContext>();
            await context.Database.MigrateAsync();
        }

        private static async Task SeedData(IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var acrossModules = scope.ServiceProvider.GetServices<ISeedData>();
            foreach (var seeder in acrossModules) 
            { 
                await seeder.SeedAsync();
            }
        }
    }
}
