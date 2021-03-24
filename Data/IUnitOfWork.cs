using Schulcast.Server.Repositories;

namespace Schulcast.Server.Data
{
	public interface IUnitOfWork
	{
		BlogRepository BlogRepository { get; }
		FileRepository FileRepository { get; }
		MemberRepository MemberRepository { get; }
		SlideRepository SlideRepository { get; }
		TaskRepository TaskRepository { get; }
		void CommitChanges();
	}
}