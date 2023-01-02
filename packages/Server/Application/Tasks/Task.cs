namespace Schulcast.Application.Tasks;

public class Task : Model
{
	[Required, MaxLength(50)] public string Title { get; set; } = null!;
	public List<MemberTask> Members { get; set; } = new();
}