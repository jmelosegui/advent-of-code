using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode2024.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterAllTypes<T>(
        this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x =>
        {
            var result = (x.GetInterfaces().Contains(typeof(T)) || x.BaseType == typeof(T))
                         && x.IsAbstract == false;
            return result;
        }));

        foreach (var type in typesFromAssemblies)
        {
            object? serviceKey = null;
            var challengeAttribute = type.GetCustomAttribute<ChallengeAttribute>();
            if (challengeAttribute != null)
            {
                serviceKey = challengeAttribute.Name;
            }

            services.Add(new ServiceDescriptor(typeof(T), serviceKey, type, lifetime));
        }

        return services;
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ChallengeAttribute : Attribute
{
    public string? Name { get; set; }
}