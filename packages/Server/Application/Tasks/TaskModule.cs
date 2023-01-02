namespace Schulcast.Application.Tasks;


public class TaskModule : Module
{
	public override void ConfigureServices(IServiceCollection services) => services.AddTransient<TaskService>();

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/task", [Authorize(Roles = MemberRoles.Admin)] (TaskService taskService) => taskService.GetAll());
		endpoints.MapGet("/api/task/{id}", [AllowAnonymous] (TaskService taskService, int id) => taskService.Get(id));
		endpoints.MapPost("/api/task", [Authorize(Roles = MemberRoles.Admin)] (TaskService taskService, Task task) => taskService.Create(task));
		endpoints.MapPut("/api/task", [Authorize(Roles = MemberRoles.Admin)] (TaskService taskService, Task task) => taskService.Update(task));
		endpoints.MapDelete("/api/task/{id}", [Authorize(Roles = MemberRoles.Admin)] (TaskService taskService, int id) => taskService.Delete(id));
	}
}