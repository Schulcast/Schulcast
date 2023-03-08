namespace Schulcast.Infrastructure.Persistence;

public class DatabaseContext : DbContext, IDatabase
{
	public DbSet<Member> Members { get; set; } = null!;
	public DbSet<Application.Tasks.Task> Tasks { get; set; } = null!;
	public DbSet<MemberData> MemberData { get; set; } = null!;
	public DbSet<Post> Posts { get; set; } = null!;
	public DbSet<Application.Files.File> Files { get; set; } = null!;
	public DbSet<Slide> Slides { get; set; } = null!;

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	public async System.Threading.Tasks.Task Initialize()
	{
		await Database.MigrateAsync();

		if (!Members.Any())
		{
			var superadmin = new Member
			{
				Nickname = "admin",
				Password = "566164961477cf9db44bf600e04483fd1a3e2213e0ac34404a06a65a0dd80a46",
				Role = MemberRoles.Admin
			};
			Members.Add(superadmin);
			await SaveChangesAsync();
		}
		if (!Tasks.Any())
		{
			var tasks = new List<Application.Tasks.Task>
				{
					new() { Title = "Schnitt & Film" },
					new() { Title = "Springer" },
					new() { Title = "Website" },
					new() { Title = "Social-Media" },
					new() { Title = "Fotos, Filming & Ton" },
					new() { Title = "Leitung" },
					new() { Title = "Sonstiges" }
				};

			Tasks.AddRange(tasks);
			await SaveChangesAsync();
		}
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Member>().HasMany(m => m.Data).WithOne(d => d.Member).HasForeignKey(d => d.MemberId).OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<MemberTask>().HasKey(mt => new { mt.MemberId, mt.TaskId });
		modelBuilder.Entity<MemberTask>().HasOne(mt => mt.Member).WithMany(m => m.Tasks).HasForeignKey(mt => mt.MemberId);
		modelBuilder.Entity<MemberTask>().HasOne(mt => mt.Task).WithMany(t => t.Members).HasForeignKey(mt => mt.TaskId);

		modelBuilder.Entity<Post>(p => p.Property(m => m.Tags)!.HasJsonConversion(Array.Empty<string>()));
	}

	public DbSet<TEntity> GetSet<TEntity>() where TEntity : class => Set<TEntity>();
	public EntityEntry GetEntry(object entity) => Entry(entity);
	public void CommitChanges() => SaveChanges();
	public System.Threading.Tasks.Task CommitChangesAsync() => SaveChangesAsync();
}