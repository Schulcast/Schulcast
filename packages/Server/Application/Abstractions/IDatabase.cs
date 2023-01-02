namespace Schulcast.Application.Abstractions;

public interface IDatabase
{
	/// <summary>
	/// This method is called by the application to initialize the database.
	/// Tasks such as seeding data and applying migrations should be done here.
	/// </summary>
	System.Threading.Tasks.Task Initialize();

	DbSet<Member> Members { get; set; }
	DbSet<Tasks.Task> Tasks { get; set; }
	DbSet<MemberData> MemberData { get; set; }
	DbSet<Post> Posts { get; set; }
	DbSet<Files.File> Files { get; set; }
	DbSet<Slide> Slides { get; set; }

	DbSet<TEntity> GetSet<TEntity>() where TEntity : class;
	EntityEntry GetEntry(object entity);
	ChangeTracker ChangeTracker { get; }
	void CommitChanges();
	System.Threading.Tasks.Task CommitChangesAsync();
}