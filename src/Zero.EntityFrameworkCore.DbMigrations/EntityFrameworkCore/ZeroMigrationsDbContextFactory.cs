using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Zero.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ZeroMigrationsDbContextFactory : IDesignTimeDbContextFactory<ZeroMigrationsDbContext>
    {
        public ZeroMigrationsDbContext CreateDbContext(string[] args)
        {
            ZeroEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ZeroMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ZeroMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
