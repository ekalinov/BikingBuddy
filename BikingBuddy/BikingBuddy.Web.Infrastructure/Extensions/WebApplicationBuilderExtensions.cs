using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BikingBuddy.Web.Infrastructure.Extensions
{
    public static  class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// The method registered all services with their interfaces and implementations of given assembly.
        /// The assembly is taken from type of random service interface or implementation.
        /// </summary>
        /// <param name="serviceType"> Type of random service implementation!</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {

            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly is null)
            {
                throw new InvalidOperationException("Invalid service type provider!");
            }

            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t=>t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
                if (interfaceType is null)
                {
                    throw new InvalidOperationException($"No interface is provided for service with name: s{implementationType.Name}!");
                }

                services.AddScoped(interfaceType, implementationType);
            }
            

        }
    }
}
