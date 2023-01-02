namespace Schulcast.Infrastructure.Identity;

public class IdentityModule : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddHttpContextAccessor();
		services.AddTransient<ICurrentUserService, CurrentUserService>();
	}
}
