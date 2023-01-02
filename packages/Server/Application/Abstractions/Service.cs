namespace Schulcast.Application.Abstractions;

public abstract class Service
{
	protected readonly IDatabase _database;
	protected readonly ICurrentUserService _currentUserService;

	public Service(IDatabase database, ICurrentUserService currentUserService)
	{
		_database = database;
		_currentUserService = currentUserService;
	}
}