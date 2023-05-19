using Infrastructure.Commands;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Infrastructure.Queries.GetAllUsers;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddAuthorizationService(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
        }

        public static void AddAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainMapper));
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            var handlerTypes = typeof(CommandDispatcher).Assembly.GetTypes()
                .Where(x => !x.IsInterface &&
                            !x.IsAbstract &&
                            x.GetInterfaces().Any(y => y.Name.Equals(typeof(IHandle<>).Name, StringComparison.InvariantCulture)
                            || y.Name.Equals(typeof(IHandle<,>).Name, StringComparison.InvariantCulture))).ToList();

            foreach (var type in handlerTypes)
            {
                foreach (var myInterface in type.GetInterfaces())
                {
                    services.AddScoped(myInterface, type);
                }
            }
            return services;
        }

        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            var queries = typeof(GetAllUsersQuery).Assembly.GetTypes()
                .Where(x => !x.IsInterface &&
                            !x.IsAbstract &&
                            x.GetInterfaces().Any(y => y.Name.Equals(typeof(IQuery<>).Name, StringComparison.InvariantCulture)
                            || y.Name.Equals(typeof(IQuery<,>).Name, StringComparison.InvariantCulture))).ToList();

            foreach (var query in queries)
            {
                foreach (var myInterface in query.GetInterfaces())
                {
                    services.AddScoped(myInterface, query);
                }
            }
            return services;
        }
    }
}
