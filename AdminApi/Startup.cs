using AutoMapper;
using Castle.DynamicProxy;
using IdentityServer4.AccessTokenValidation;
using Is4.Common.Extensions;
using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.EFCore.MySql;
using Is4.EFCore.MySql.Extensions;
using Is4.EFCore.Shared;
using Is4.Service.Implement;
using Is4.Service.Interceptor;
using Is4.Service.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdminApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMySqlDbContexts(Configuration);

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAsyncInterceptor, AutoUnitOfWorkInterceptor>();

            services.AddAppService();
            services.AddRepository();
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddAuthentication(options => {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:5001";
                options.ApiName = "AdminApi";
                options.RequireHttpsMetadata = false;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
