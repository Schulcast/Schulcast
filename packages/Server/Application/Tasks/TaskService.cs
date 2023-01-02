namespace Schulcast.Application.Tasks;

public class TaskService : CrudService<Task>
{
	public TaskService(IDatabase database, ICurrentUserService currentUserService) : base(database, currentUserService) { }
}