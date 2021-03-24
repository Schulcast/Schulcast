using System.Collections.Generic;
using System.IO;
using System.Linq;
using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Repositories
{
	public class FileRepository : RepositoryBase<Models.File>
	{
		public const string uploadDirectoryName = "uploads";
		public static readonly string uploadDirectory = $"{Program.Environment.ContentRootPath}/{uploadDirectoryName}/";

		public FileRepository(DatabaseContext database) : base(database)
		{
			Directory.CreateDirectory(uploadDirectory);
			Directory.CreateDirectory(uploadDirectory + FileDirectories.Slides);
			Directory.CreateDirectory(uploadDirectory + FileDirectories.Members);
			Directory.CreateDirectory(uploadDirectory + FileDirectories.Misc);
		}

		public List<Models.File> GetByFolder(string folder)
		{
			return database.Files.Where(f => f.Path.Contains(folder)).ToList();
		}

		public void AddAndWrite() {
			
		}

		public override void Delete(Models.File file)
		{
			var path = uploadDirectory + file.Path;
			System.IO.File.Delete(path);
			base.Delete(file);
		}
	}
}