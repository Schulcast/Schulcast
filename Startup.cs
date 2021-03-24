using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schulcast.Server.Data;
using Schulcast.Server.Installers;
using Schulcast.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schulcast.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Program.Configuration = configuration;
			MigrateDatabase();
		}

		static DatabaseContext MigrateDatabase()
		{
			var database = new DatabaseContext();
			database.Database.Migrate();
			if (!database.Members.Any())
			{
				var superadmin = new Member
				{
					Nickname = "admin",
					Password = "566164961477cf9db44bf600e04483fd1a3e2213e0ac34404a06a65a0dd80a46",
					Role = MemberRoles.Admin
				};
				database.Members.Add(superadmin);
				database.SaveChanges();
			}
			if (!database.Tasks.Any())
			{
				var tasks = new List<Task>
				{
					new Task { Title = "Schnitt & Film" },
					new Task { Title = "Springer" },
					new Task { Title = "Website" },
					new Task { Title = "Social-Media" },
					new Task { Title = "Fotos, Filming & Ton" },
					new Task { Title = "Leitung" },
					new Task { Title = "Sonstiges" }
				};

				database.Tasks.AddRange(tasks);
				database.SaveChanges();
			}

			return database;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			Program.YoutubeStorage = (null, DateTime.Now.AddYears(-1));

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