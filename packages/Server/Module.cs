namespace Schulcast;

public abstract class Module
{
	public static List<Module> All => typeof(Module).Assembly
		.GetTypes()
		.Where(p => p.IsClass && p.IsAssignableTo(typeof(Module)) && p.IsAbstract == false)
		.Select(Activator.CreateInstance)
		.Cast<Module>()
		.ToList();

	public virtual void ConfigureServices(IServiceCollection services) { }
	public virtual void ConfigureApplication(WebApplication application) { }
	public virtual void ConfigureEndpoints(IEndpointRouteBuilder endpoints) { }
}

public static class ModuleExtensions
{
	public static WebApplicationBuilder InstallSchulcast(this WebApplicationBuilder builder)
	{
		builder.Services.ConfigureSchulcastModulesServices();
		return builder;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "ASP0014", Justification = "This is a wrapper around all the modules.")]
	public static WebApplication ConfigureSchulcast(this WebApplication application)
	{
		application.ConfigureSchulcastModulesApplication();
		application.UseEndpoints(endpoints => endpoints.ConfigureSchulcastModulesEndpoints());
		return application;
	}

	private static void ConfigureSchulcastModulesServices(this IServiceCollection services)
		=> Module.All.ForEach(module => module.ConfigureServices(services));

	private static void ConfigureSchulcastModulesApplication(this WebApplication application)
		=> Module.All.ForEach(module => module.ConfigureApplication(application));

	private static void ConfigureSchulcastModulesEndpoints(this IEndpointRouteBuilder endpoints)
		=> Module.All.ForEach(module => module.ConfigureEndpoints(endpoints));
}