using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.AuthorizationChecker.Options;

namespace SharpGrip.AuthorizationChecker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationChecker(this IServiceCollection services, Action<AuthorizationCheckerOptions>? setupOptions = null)
        {
            services.AddOptions();
            services.AddScoped<IAuthorizationChecker, AuthorizationChecker>();

            var voterTypes = Assembly.GetCallingAssembly().GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IVoter)) && type.IsClass && !type.IsAbstract);

            foreach (var voterType in voterTypes)
            {
                services.AddTransient(voterType.GetInterfaces().First(type => type.IsGenericType), voterType);
            }

            if (setupOptions != null)
            {
                services.Configure(setupOptions);
            }

            return services;
        }
    }
}