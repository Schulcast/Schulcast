using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Repositories
{
	public class SlideRepository : RepositoryBase<Slide>
	{
		public SlideRepository(DatabaseContext database) : base(database) { }
	}
}