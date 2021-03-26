using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Schulcast.Server
{
	public class Program
	{
		public static IConfiguration Configuration { get; set; } = null!;
		public static IWebHostEnvironment Environment { get; set; } = null!;

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
		}
	}
}