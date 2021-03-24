using Microsoft.Extensions.DependencyInjection;
using Schulcast.Server.Data;

namespace Schulcast.Server.Installers
{
	public class DatabaseInstaller : IInstaller
	{
		public void Install(IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>();
		}
	}
}
