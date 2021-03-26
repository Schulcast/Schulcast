using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schulcast.Server.Data;
using Schulcast.Server.Helpers;
using Schulcast.Server.Installers;

namespace Schulcast.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Program.Configuration = configuration;
			using var database = new DatabaseContext();
			database.Database.Migrate();
			database.SeedSchulcastDataIfEmpty();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.InstallSchulcast()
				.AddControllers()
				.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			Program.Environment = env;
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
				.UseRouting()
				.UseAuthentication()
				.UseAuthorization()
				.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}