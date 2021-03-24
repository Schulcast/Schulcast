using System.Linq;
using Schulcast.Server.Data;

public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
	public int AuthenticatedAccountId => User.Claims.Count() == 0 ? 0 : int.Parse(User.Claims.ElementAt(1).Value);

	internal UnitOfWork UnitOfWork { get; }
	public ControllerBase(UnitOfWork unitOfWork)
	{
		UnitOfWork = unitOfWork;
	}
}