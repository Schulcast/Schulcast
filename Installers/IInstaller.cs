using Microsoft.Extensions.DependencyInjection;

namespace Schulcast.Server.Installers
{
	interface IInstaller
	{
		void Install(IServiceCollection services);
	}
}