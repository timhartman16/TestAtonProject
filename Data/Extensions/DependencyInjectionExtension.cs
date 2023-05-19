using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Data.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var repositories = typeof(UserRepository).Assembly.GetTypes()
                .Where(x => x.Name.EndsWith("Repository") && !x.GetTypeInfo().IsAbstract).ToList();

            foreach (var repository in repositories)
            {
                foreach (var interfaceRepo in repository.GetInterfaces())
                {
                    services.AddScoped(interfaceRepo, repository);
                }
            }
            return services;
        }
    }
}
