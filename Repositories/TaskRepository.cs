using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Repositories
{
	public class TaskRepository : RepositoryBase<Task>
	{
		public TaskRepository(DatabaseContext database) : base(database) { }
	}
}