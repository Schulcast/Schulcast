using Microsoft.Extensions.FileProviders;
using File = System.IO.File;

namespace Schulcast.Application.Client;

public class ClientModule : Module
{
	private static readonly string BasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "client");

	public override void ConfigureApplication(WebApplication application)
	{
		application.UseStaticFiles(new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(BasePath),
			ServeUnknownFileTypes = true,
		});
	}

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapFallback(HtmlResult).AllowAnonymous();
	}

	private async System.Threading.Tasks.Task HtmlResult(HttpContext context)
	{
		var filePath = Path.Combine(BasePath, context.Request.Path.Value ?? "");

		if (File.Exists(filePath))
		{
			var file = await File.ReadAllBytesAsync(filePath);
			var fileName = Path.GetFileName(filePath);
			new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);
			context.Response.ContentType = contentType;
			await context.Response.Body.WriteAsync(file);
		}

		var indexPath = Path.Combine(BasePath, "index.html");
		var html = await File.ReadAllTextAsync(indexPath);
		context.Response.ContentType = "text/html";

		if (EnvironmentVariables.BASE_URL.Contains("localhost"))
		{
			context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
			context.Response.Headers.Add("Expires", "-1");
		}

		await context.Response.WriteAsync(html);
	}
}