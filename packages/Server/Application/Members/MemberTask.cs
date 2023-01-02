namespace Schulcast.Application.Members;

public class MemberTask
{
	public int TaskId { get; set; }
	public Tasks.Task? Task { get; set; }
	public int MemberId { get; set; }
	public Member? Member { get; set; }
}