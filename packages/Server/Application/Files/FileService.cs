namespace Schulcast.Application.Files;

public class FileService : CrudService<File>
{
	public const string uploadDirectoryName = "uploads";
	public static readonly string uploadDirectory = $"/{uploadDirectoryName}/";

	public FileService(IDatabase database, ICurrentUserService currentUserService) : base(database, currentUserService)
	{
		Directory.CreateDirectory(uploadDirectory);
		Directory.CreateDirectory(uploadDirectory + FileDirectories.Slides);
		Directory.CreateDirectory(uploadDirectory + FileDirectories.Members);
		Directory.CreateDirectory(uploadDirectory + FileDirectories.Misc);
	}

	public List<File> GetByFolder(string folder)
	{
		return _database.Files.Where(f => f.Path.Contains(folder)).ToList();
	}

	public override async Task<File> Delete(int id)
	{
		var file = await Get(id);
		var path = uploadDirectory + file.Path;
		System.IO.File.Delete(path);
		return await base.Delete(file.Id);
	}
}