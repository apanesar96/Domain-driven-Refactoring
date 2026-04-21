namespace BrewUp.Rest.Modules;

public interface IModule
{
    public IServiceCollection Register(WebApplicationBuilder builder);
}