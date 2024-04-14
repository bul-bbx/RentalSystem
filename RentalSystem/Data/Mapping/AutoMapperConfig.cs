using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // Ensure you import this namespace

namespace RentalSystem.Data.Mapping
{
    public class AutoMapperConfig
    {
        private static bool isInitialized;

        public static void ConfigureMapping(IServiceCollection services)
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;

            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().FullName.Contains(nameof(RentalSystem)))
                .SelectMany(a => a.GetTypes())
                .ToArray();

            var allMappingTypes = allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.GetInterfaces()
                                .Where(i => i.IsGenericType)
                                .Select(i => i.GetGenericTypeDefinition())
                                .Contains(typeof(IMapWith<>)))
                .Select(t => new
                {
                    Type1 = t,
                    Type2 = t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapWith<>))
                        .SelectMany(i => i.Arguments)
                        .First()
                })
                .ToList();

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                foreach (var type in allMappingTypes)
                {
                    config.CreateMap(type.Type1, type.Type2);
                    config.CreateMap(type.Type2, type.Type1);
                }

                allTypes
                    .Where(t => t.IsClass
                                && !t.IsAbstract
                                && typeof(IHaveCustomMapping).IsAssignableFrom(t))
                    .Select(Activator.CreateInstance)
                    .Cast<IHaveCustomMapping>()
                    .ToList()
                    .ForEach(mapping => mapping.ConfigureMapping(config));
            });

            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper); 
        }
    }
}