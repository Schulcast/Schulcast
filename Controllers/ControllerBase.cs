using Schulcast.Server.Data;
using System.Linq;

namespace Schulcast.Core.Controllers
{
	public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		public int AuthenticatedAccountId => !User.Claims.Any() ? 0 : int.Parse(User.Claims.ElementAt(1).Value);

		internal UnitOfWork UnitOfWork { get; }
		public ControllerBase(UnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}
	}
}