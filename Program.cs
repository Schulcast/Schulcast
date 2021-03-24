using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Schulcast.Server
{
	public class Program
	{
		public static IConfiguration Configuration { get; set; }
		public static IWebHostEnvironment Environment { get; set; }
		public static (string json, DateTime lastResponse) YoutubeStorage { get; set; }
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
	}
}