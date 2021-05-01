using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Schulcast.Server.Installers
{
	public class OpenApiInstaller : IInstaller
	{
		public void Install(IServiceCollection services)
		{
			services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Schulcast", Version = "1" }));
		}
	}
}