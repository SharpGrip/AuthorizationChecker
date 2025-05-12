using System;
using System.Collections.Generic;
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

            var voterTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(LoadTypes).Where(type => typeof(IVoter).IsAssignableFrom(type) && type.IsClass && type.IsPublic && !type.IsAbstract);

            foreach (var voterType in voterTypes)
            {
                foreach (var voterInterface in voterType.GetInterfaces().Where(type => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IVoter<>)))
                {
                    services.AddTransient(voterInterface, voterType);
                }
            }

            if (setupOptions != null)
            {
                services.Configure(setupOptions);
            }

            return services;
        }

        private static IEnumerable<Type> LoadTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                return reflectionTypeLoadException.Types.Where(type => type != null);
            }
        }
    }
}