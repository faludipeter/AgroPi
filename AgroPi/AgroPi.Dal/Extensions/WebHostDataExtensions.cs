using AgroPi.Dal.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;

namespace AgroPi.Dal.Extensions
{
    public static class WebHostDataExtensions
    {
        public static IWebHost SeedAdministrator(this IWebHost host)
            => host.Scoped<RolesAndAdministratorSeeder>((s, l) => s.GetRequiredService<RolesAndAdministratorSeeder>().Seed().GetAwaiter().GetResult(), "Seeding Administrator as needed");


        private static IWebHost Scoped<TLog>(this IWebHost host, Action<IServiceProvider, ILogger<TLog>> action, string title)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<TLog>>();
                try
                {
                    action(serviceProvider, logger);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred during action: {title}");
                }
            }
            return host;
        }
    }
}
