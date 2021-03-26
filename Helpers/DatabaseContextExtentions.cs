using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Data;
using Schulcast.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Schulcast.Server.Helpers
{
	public static class RepositoryExtensions
	{
		public static IQueryable<T> Eager<T>(this DatabaseContext database) where T : class
		{
			var query = database.Set<T>().AsQueryable();

			var navigations = database.Model.FindEntityType(typeof(T))
				.GetDerivedTypesInclusive()
				.SelectMany(type => type.GetNavigations())
				.Distinct();

			foreach (var property in navigations)
			{
				query = query.Include(property.Name);
			}

			return query;
		}

		public static DatabaseContext SeedSchulcastDataIfEmpty(this DatabaseContext database)
		{
			if (!database.Members.Any())
			{
				var superadmin = new Member
				{
					Nickname = "admin",
					Password = "566164961477cf9db44bf600e04483fd1a3e2213e0ac34404a06a65a0dd80a46",
					Role = MemberRoles.Admin
				};
				database.Members.Add(superadmin);
				database.SaveChanges();
			}
			if (!database.Tasks.Any())
			{
				var tasks = new List<Task>
				{
					new Task { Title = "Schnitt & Film" },
					new Task { Title = "Springer" },
					new Task { Title = "Website" },
					new Task { Title = "Social-Media" },
					new Task { Title = "Fotos, Filming & Ton" },
					new Task { Title = "Leitung" },
					new Task { Title = "Sonstiges" }
				};

				database.Tasks.AddRange(tasks);
				database.SaveChanges();
			}
			return database;
		}
	}
}