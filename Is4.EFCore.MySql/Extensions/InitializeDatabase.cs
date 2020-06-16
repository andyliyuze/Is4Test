using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Is4.Domain;
using Is4.EFCore.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql.Extensions
{
    public static class InitializeDatabase
    {
        public static async Task Run(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //await serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //await context.Database.MigrateAsync();

                //var context2 = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                //await context2.Database.MigrateAsync();

                
                //var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var users = new List<User>() { new User() { UserName = "bob" } };
                //foreach (var user in users)
                //{
                //    var identityUser = new User
                //    {
                //        UserName = user.UserName,
                //        Email = user.Email,
                //        EmailConfirmed = true
                //    };

                //    // if there is no password we create user without password
                //    // user can reset password later, because accounts have EmailConfirmed set to true

                //    await userManager.CreateAsync(identityUser, "Pass123$");

                //}

                foreach (var item in Config.Apis)
                {
                    context.ApiResources.Add(item.ToEntity());
                    context.SaveChanges();
                }
                foreach (var item in Config.Clients)
                {
                    context.Add(item.ToEntity());
                    context.SaveChanges();
                }
                foreach (var item in Config.Ids)
                {
                    context.Add(item.ToEntity());
                    context.SaveChanges();
                }
            }
        }
    }
}
