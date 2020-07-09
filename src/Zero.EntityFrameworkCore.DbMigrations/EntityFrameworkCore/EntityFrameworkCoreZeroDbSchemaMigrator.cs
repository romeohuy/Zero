using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zero.Data;
using Volo.Abp.DependencyInjection;

namespace Zero.EntityFrameworkCore
{
    public class EntityFrameworkCoreZeroDbSchemaMigrator
        : IZeroDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreZeroDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ZeroMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ZeroMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}