using Is4.Common.Extensions;
using Is4.Domain;
using Is4.EFCore.MySql.Extensions;
using Is4.EFCore.Shared;
using Is4Test.Extensions;
using Is4Test.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Senparc.Weixin.RegisterServices;

namespace Is4Test
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
            services.AddSenparcWeixinServices(Configuration);//Senparc.Weixin ע�ᣨ���룩  

            services.AddMySqlDbContexts(Configuration);
            services.AddIdentity<User, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            //    .AddTestUsers(TestUsers.Users).AddInMemoryApiResources(Config.Apis).AddInMemoryClients(Config.Clients)
            //.AddInMemoryIdentityResources(Config.Ids);
            .AddConfigurationStore().AddOperationalStore().AddConfigurationStoreCache();

            var identitybuilder = builder.AddAspNetIdentity<User>()
                .AddProfileService<ImplicitProfileService>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomUserClaimsPrincipalFactory>();

            builder.AddDeveloperSigningCredential();
            services.AddControllersWithViews();

            services.AddMasstransitService(x =>
            {
                //���ò�Ʋ��Ǳ����
                //x.AddConsumer<UpdateClientConsumer>();
            }, (cfg, serviceProvider) =>
             {
                 cfg.ReceiveEndpoint("customer_update_queue", e =>
                 {
                     e.Bind("value-enterd-exchange");
                     e.Consumer(typeof(UpdateClientConsumer), a =>
                     {
                         //ÿ���յ�һ����Ϣ������һ����������
                         var _cache = serviceProvider.GetService<IMemoryCache>();
                         return new UpdateClientConsumer(_cache);
                     });
                 });
             }, hostService: false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
              //  InitializeDatabase.Run(app).Wait();
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
          //  app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
