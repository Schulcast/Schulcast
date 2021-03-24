using Schulcast.Server.Repositories;

namespace Schulcast.Server.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		public BlogRepository BlogRepository { get; private set; }
		public FileRepository FileRepository { get; private set; }
		public MemberRepository MemberRepository { get; private set; }
		public SlideRepository SlideRepository { get; private set; }
		public TaskRepository TaskRepository { get; private set; }

		readonly DatabaseContext database;
		public UnitOfWork(DatabaseContext database)
		{
			this.database = database;
			BlogRepository = new BlogRepository(database);
			FileRepository = new FileRepository(database);
			MemberRepository = new MemberRepository(database);
			SlideRepository = new SlideRepository(database);
			TaskRepository = new TaskRepository(database);
		}

		public void CommitChanges()
		{
			database.SaveChanges();
		}
	}
}