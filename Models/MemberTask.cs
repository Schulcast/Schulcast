namespace Schulcast.Server.Models
{
	public class MemberTask
	{
		public int TaskId { get; set; }
		public Task Task { get; set; } = null!;
		public int MemberId { get; set; }
		public Member Member { get; set; } = null!;
	}
}