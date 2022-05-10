using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Ordermanager.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime LifeTime { get; set; }
        public ServiceAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            LifeTime = serviceLifetime;
        }
    }
    public static class ServiceCollectionExpand
    {
        /// <summary>
        /// 按特性中的生命周期注入业务组件
        /// </summary>
        /// <param name="service"></param>
        public static void AddBusiness(this IServiceCollection service)
        {
            //获取有ServiceAttribute特性的所有类
            List<Type> types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract
                                      && t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0
                )
                .ToList();

            types.ForEach(impl =>
            {
                //获取该类所继承的所有接口
                Type[] interfaces = impl.GetInterfaces();
                //获取该类注入的生命周期
                var lifetime = impl.GetCustomAttribute<ServiceAttribute>().LifeTime;

                interfaces.ToList().ForEach(i =>
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            service.AddSingleton(i, impl);
                            break;
                        case ServiceLifetime.Scoped:
                            service.AddScoped(i, impl);
                            break;
                        case ServiceLifetime.Transient:
                            service.AddTransient(i, impl);
                            break;
                    }
                });
            });

        }

    }
}
