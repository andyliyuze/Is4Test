using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Is4.EFCore.MySql.Extensions;
using Microsoft.AspNetCore.Identity;
using Is4.EFCore.Shared;
using Is4.Domain;
using Is4.EFCore.MySql;
using Is4.Domain.Repostitory;
using Is4.Service.Interceptor;
using Castle.DynamicProxy;
using Is4.Common.Extensions;
using AutoMapper;
using Is4.Service.Shared;

namespace Is4.ServiceTests
{
    public class UnitTestBase
    {
        protected ServiceProvider ServiceProvider { get; set; }
        public UnitTestBase() 
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var Configuration = builder.Build();
            IServiceCollection services = new ServiceCollection();

            services.AddMySqlDbContexts(Configuration);

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAsyncInterceptor, AutoUnitOfWorkInterceptor>();

            services.AddAppService();
            services.AddRepository();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddControllers();

            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Test1()
        {
           
        }
    }
}
