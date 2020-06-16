using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Is4.EFCore.Shared
{

    //public class AppConfigurationDbContext : ConfigurationDbContext
    //{
    //    public AppConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
         
    //        builder.Entity<IdentityClaim>().ToTable("IdentityClaims");

    //        // Customize the ASP.NET Identity model and override the defaults if needed.
    //        // For example, you can rename the ASP.NET Identity table names and more.
    //        // Add your customizations after calling base.OnModelCreating(builder);
    //    }
    //}
}
