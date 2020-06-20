using Castle.DynamicProxy;
using Is4.Domain.Shared.Events;
using Is4.Shared;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
namespace Is4.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddAppService(this IServiceCollection services)
        {
            Type iNeedInject = typeof(IAppService);
            //接口程序集
            string assemblyInterName = "Is4.Service.Shared";
            //实现类程序集
            string assemblyObjName = "Is4.Service";
            var interTypes = Assembly.Load(assemblyInterName).GetTypes().Where(t => t.IsInterface && iNeedInject.IsAssignableFrom(t)).ToArray();
            var implTypes = Assembly.Load(assemblyObjName).GetTypes();

            //如果注册了拦截器
            var provider = services.BuildServiceProvider();

            foreach (var interType in interTypes)
            {
                var implType = implTypes.Where(t => t.IsClass && interType.IsAssignableFrom(t)).SingleOrDefault();
                if (implType == null)
                {
                    continue;
                }
                //确定是否可以将指定类型A的实例分配给变量当前类型B
                if (typeof(IAutoUnitOfWork).IsAssignableFrom(interType))
                {
                    //注册实现类
                    services.AddScoped(implType);
                    //注册接口
                    services.AddScoped(interType, a =>
                   {
                       var interceptors = a.GetServices<IAsyncInterceptor>().ToArray();
                       if (!interceptors.Any()) { throw new Exception("未注册工作单元拦截器"); }
                       var impl = a.GetService(implType);
                       var generator = new ProxyGenerator();
                       var proxy = generator.CreateInterfaceProxyWithTargetInterface(interType, impl, interceptors);
                       return proxy;
                   });
                }
                else
                {
                    services.AddScoped(interType, implType); ;
                }
            }
        }

        public static void AddRepository(this IServiceCollection services)
        {
            Type iNeedInject = typeof(IRepository);
            //接口程序集
            string assemblyInterName = "Is4.Domain";
            //实现类程序集
            string assemblyObjName = "Is4.EFCore.MySql";
            var interTypes = Assembly.Load(assemblyInterName).GetTypes().Where(t => t.IsInterface && iNeedInject.IsAssignableFrom(t)).ToArray();
            var implTypes = Assembly.Load(assemblyObjName).GetTypes();

            foreach (var interType in interTypes)
            {
                var implType = implTypes.Where(t => t.IsClass && interType.IsAssignableFrom(t)).SingleOrDefault();
                if (implType == null)
                {
                    continue;
                }

                services.AddScoped(interType, implType); ;
            }
        }

        public static void AddMasstransitService(this IServiceCollection services, Action<IServiceCollectionBusConfigurator> configuratorx = null, Action<IRabbitMqBusFactoryConfigurator, ServiceProvider> configurator = null, bool hostService = false)
        {
            services.AddMassTransit(x =>
            {
                configuratorx?.Invoke(x);
               
                x.AddBus(context =>
                 {
                     var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                          {
                              cfg.Host(new Uri("amqp://102.17.1.164:30579"));

                              cfg.ConfigureJsonDeserializer(settings =>
                              {
                                  settings.TypeNameHandling = TypeNameHandling.Auto;
                                  return settings;
                              });
                              cfg.Message<ValueEntered>(x =>
                              {
                                  x.SetEntityName("value-enterd-exchange");
                              });
                              configurator?.Invoke(cfg, x.Collection.BuildServiceProvider());
                          });
                     //if (!hostService)
                     //{
                     //    busControl.StartAsync();
                     //}
                     //else
                     //{
                         
                     //}
                     return busControl;
                 });
                
            });
            services.AddMassTransitHostedService();
        }
    }


    public interface IRegisterAppServiceBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }

    public class RegisterAppServiceBuilder : IRegisterAppServiceBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityServerBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <exception cref="System.ArgumentNullException">services</exception>
        public RegisterAppServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
        public IServiceCollection Services { get; }
    }
}