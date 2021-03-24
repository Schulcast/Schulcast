using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Repositories
{
	public class BlogRepository : RepositoryBase<Post>
	{
		public BlogRepository(DatabaseContext database) : base(database) { }
	}
}