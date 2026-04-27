using System.Reflection;

namespace BrewUp.Rest.Modules;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder RegisterModules(this WebApplicationBuilder builder)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsClass && type.IsAssignableTo(typeof(IModule)))
            .Select(module => (IModule)Activator.CreateInstance(module)!).ToList()
            .ForEach(module => module.Register(builder));
        
        return builder;
    }
}