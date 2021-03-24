using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Models;

namespace Schulcast.Server.Data
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Member> Members { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<MemberData> MemberData { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<File> Files { get; set; }
		public DbSet<Slide> Slides { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(Program.Configuration["Database:ConnectionString"]);
			//optionsBuilder.UseSqlServer(Program.Configuration["Database:MSSQL"]);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>().HasMany(m => m.Data).WithOne(d => d.Member).HasForeignKey(d => d.MemberId).OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MemberTask>().HasKey(mt => new { mt.MemberId, mt.TaskId });
			modelBuilder.Entity<MemberTask>().HasOne(mt => mt.Member).WithMany(m => m.Tasks).HasForeignKey(mt => mt.MemberId);
			modelBuilder.Entity<MemberTask>().HasOne(mt => mt.Task).WithMany(t => t.Members).HasForeignKey(mt => mt.TaskId);
		}
	}
}