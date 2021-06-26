using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SmartAC.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.Web.Extensions
{
    public static class DatabaseExtensions
    {
        public static IHost MigrateDb(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = host.Services.GetRequiredService<ILogger<DataContext>>();
                logger.LogError(ex, "An error occurred migrating the the DB.");
            }
            return host;
        }
    }
}
