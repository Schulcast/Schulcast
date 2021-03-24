using Microsoft.Extensions.DependencyInjection;
using Schulcast.Server.Data;
using Schulcast.Server.Repositories;

namespace Schulcast.Server.Installers
{
	public class RepositoriesInstaller : IInstaller
	{
		public void Install(IServiceCollection services)
		{
			services.AddTransient<BlogRepository>();
			services.AddTransient<FileRepository>();
			services.AddTransient<MemberRepository>();
			services.AddTransient<SlideRepository>();
			services.AddTransient<TaskRepository>();
			services.AddTransient<UnitOfWork>();
		}
	}
}