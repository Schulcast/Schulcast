using Microsoft.Extensions.DependencyInjection;

namespace Schulcast.Server.Installers
{
	public static class Installer
	{
		public static IServiceCollection InstallSchulcast(this IServiceCollection services)
		{
			new DatabaseInstaller().Install(services);
			new RepositoriesInstaller().Install(services);
			new AuthenticationInstaller().Install(services);
			return services;
		}
	}
}