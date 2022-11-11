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
using Senparc.Weixin.RegisterServices;

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
            services.AddSenparcWeixinServices(Configuration);//Senparc.Weixin ×¢²á£¨±ØÐë£©  

            services.AddMySqlDbContexts(Configuration);

            services.AddCors(option => option.AddPolicy("cors", policy =>
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*")));

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAsyncInterceptor, AutoUnitOfWorkInterceptor>();

            services.AddAppService();
            services.AddRepository();
            services.AddMasstransitService();
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(options =>
            {
                // options.Authority = "http://192.168.43.149:5000";
                options.Authority = "http://localhost:5000";
                options.ApiName = "AdminApi";
                options.ApiSecret = "secret";
                options.RequireHttpsMetadata = false;
            });

            services.AddControllers();
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cors");
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
