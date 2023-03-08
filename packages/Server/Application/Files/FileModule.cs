namespace Schulcast.Application.Files;

public class FileModule : Module
{
	public override void ConfigureServices(IServiceCollection services) => services.AddTransient<FileService>();

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/files/{id}", [AllowAnonymous] async (FileService fileService, int id) =>
		{
			var file = await fileService.Get(id);
			return Results.File(System.IO.File.OpenRead(file.Path), "image/jpeg");
		});

		endpoints.MapPost("/api/files/{directory}", [Authorize(Roles = MemberRoles.Admin)] async (FileService fileService, string directory, HttpRequest request) =>
		{
			var formFile = request.Form.Files.GetFile("file");

			if (formFile is null)
			{
				throw new Exception("No file");
			}

			if (formFile.ContentType.Contains("image") is false)
			{
				throw new Exception("Only JPG files are allowed");
			}

			if (formFile.Length > 3_000_000)
			{
				throw new Exception("Only files smaller than 1MB are allowed");
			}

			var file = new File
			{
				Path = $"{FileService.uploadDirectoryName}/{directory}/{Guid.NewGuid()}.jpg"
			};

			using var stream = System.IO.File.Create(file.Path);
			formFile.CopyTo(stream);
			await fileService.Create(file);

			return file;
		});

		endpoints.MapDelete("/api/files/{id}", [Authorize(Roles = MemberRoles.Admin)] (FileService fileService, int id) => fileService.Delete(id));
	}
}