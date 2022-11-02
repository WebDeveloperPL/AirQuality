using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AirQuality.Core.DependencyInjection
{
    public static class AutoRegister
    {
        public static void Register(this IServiceCollection services, string namespacePattern)
        {
            var loadedAssemblies = GetLoadedAssemblies(namespacePattern);

            var scoped = GetTypes<RegisterScopedAttribute>(loadedAssemblies, namespacePattern);
            foreach (var type in scoped)
            {
                var _interface = type.Type.GetInterfaces().FirstOrDefault();
                var _type = type.Type;
                services.AddScoped(_interface, _type);
            }

            var singletons = GetTypes<RegisterSingletonAttribute>(loadedAssemblies, namespacePattern);
            foreach (var type in singletons)
            {
                var _interface = type.Type.GetInterfaces().FirstOrDefault();
                var _type = type.Type;
                services.AddSingleton(_interface, _type);
            }
        }

        public static void RegisterSingletons(this IServiceCollection services, string namespacePattern)
        {
            var loadedAssemblies = GetLoadedAssemblies(namespacePattern);

            var singletons = GetTypes<RegisterSingletonAttribute>(loadedAssemblies, namespacePattern);
            foreach (var type in singletons)
            {
                var _interface = type.Type.GetInterfaces().FirstOrDefault();
                var _type = type.Type;
                services.AddSingleton(_interface, _type);
            }
        }

        public static void RegisterScoped(this IServiceCollection services, string namespacePattern)
        {
            var loadedAssemblies = GetLoadedAssemblies(namespacePattern);

            var scoped = GetTypes<RegisterScopedAttribute>(loadedAssemblies, namespacePattern);
            foreach (var type in scoped)
            {
                var _interface = type.Type.GetInterfaces().FirstOrDefault();
                var _type = type.Type;
                services.AddScoped(_interface, _type);
            }
        }

        public static List<Assembly> GetLoadedAssemblies(string namespacePattern)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(namespacePattern))
                .ToList();

            var isAnyNewAssembly = true;
            var toLoad = loadedAssemblies.SelectMany(x => x.GetReferencedAssemblies())
                .Where(x => x.FullName.Contains(namespacePattern))
                .Where(y => loadedAssemblies.Any(a => a.FullName == y.FullName) == false).Distinct().ToList();
            isAnyNewAssembly = toLoad.Any();
            while (isAnyNewAssembly)
            {
                toLoad.ForEach(x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x)));
                toLoad = loadedAssemblies.SelectMany(x => x.GetReferencedAssemblies())
                    .Where(x => x.FullName.Contains(namespacePattern))
                    .Where(y => loadedAssemblies.Any(a => a.FullName == y.FullName) == false).Distinct().ToList();
                isAnyNewAssembly = toLoad.Any();
            }

            return loadedAssemblies;

        }

        public static List<RegistrationType> GetTypes<T>(List<Assembly> loadedAssemblies, string namespacePattern) where T : Attribute
        {
            var types =
                (from a in loadedAssemblies
                 from t in a.GetTypes().Where(x => x.Assembly.FullName.Contains(namespacePattern))
                 let attributes = t.GetCustomAttributes(typeof(T), true)
                 where attributes != null && attributes.Length > 0
                 select new RegistrationType { Type = t, Attributes = new List<Attribute>(attributes.Cast<T>().ToList()) }).ToList();

            return types;
        }
    }
}