namespace Schulcast.Infrastructure.Persistence;

public class PersistenceModule : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<IDatabase, DatabaseContext>(options => options.UseSqlite("Data Source=database/schulcast.db"));
	}

	public override void ConfigureApplication(WebApplication application)
	{
		application.Services.GetRequiredService<IDatabase>().Initialize();
	}
}