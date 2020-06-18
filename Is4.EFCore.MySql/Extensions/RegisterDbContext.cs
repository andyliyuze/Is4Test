using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Storage;
using Is4.EFCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Is4.EFCore.MySql.Extensions
{
    public static class RegisterDbContext
    {
        public static void AddMySqlDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var migrationsAssembly = typeof(RegisterDbContext).GetTypeInfo().Assembly.GetName().Name;

            // Config DB for identity
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<ConfigurationDbContext>();
            // Config DB from existing connection
            services.AddConfigurationDbContext(options => options.ConfigureDbContext = b =>
            {
                b.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            });
            //PersistedGrantDbContext
            // Operational DB from existing connection
            services.AddOperationalDbContext(options => options.ConfigureDbContext = b => b.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
        }
    }
}
