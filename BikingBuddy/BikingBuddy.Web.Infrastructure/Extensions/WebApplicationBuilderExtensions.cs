using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BikingBuddy.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using static BikingBuddy.Common.GlobalConstants;

namespace BikingBuddy.Web.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
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
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();

            foreach (Type implementationType in serviceTypes)
            {
                Type? interfaceType = implementationType.GetInterface($"I{implementationType.Name}");
                if (interfaceType is null)
                {
                    throw new InvalidOperationException(
                        $"No interface is provided for service with name: s{implementationType.Name}!");
                }

                services.AddScoped(interfaceType, implementationType);
            }
        }

        /// <summary>
        /// This method seed Administrator role if it does not exist.
        /// WARNING: Passed email must be valid email of existing user in the application 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole<Guid>> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
                {
                    if (!await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);

                        await roleManager.CreateAsync(role);
                    }

                    AppUser adminUser = await userManager.FindByEmailAsync(email);

                    if (adminUser != null &&
                        !userManager.IsInRoleAsync(adminUser, AdminRoleName).GetAwaiter().GetResult())
                    {
                        var newSecurityStamp = await userManager.UpdateSecurityStampAsync(adminUser);

                        await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                    }
                })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}