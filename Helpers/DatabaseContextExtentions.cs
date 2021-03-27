using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Schulcast.Server.Data;
using Schulcast.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Schulcast.Server.Helpers
{
	public static class RepositoryExtensions
	{
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

		public static IQueryable<T> Eager<T>(this DatabaseContext database) where T : class
		{
			var query = database.Set<T>().AsQueryable();

			var navigations = database.GetNavigationProperties(typeof(T));

			foreach (var propertyName in navigations)
			{
				query = query.Include(propertyName);
			}

			return query;
		}

		static IEnumerable<string> GetNavigationProperties(this DbContext database, Type type, int maxDepth = int.MaxValue)
		{
			if (maxDepth < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(maxDepth));
			}

			var entityType = database.Model.FindEntityType(type);
			var includedNavigations = new HashSet<INavigation>();
			var stack = new Stack<IEnumerator<INavigation>>();
			while (true)
			{
				var entityNavigations = new List<INavigation>();
				if (stack.Count <= maxDepth)
				{
					foreach (var navigation in entityType.GetNavigations())
					{
						if (includedNavigations.Add(navigation))
						{
							entityNavigations.Add(navigation);
						}
					}
				}
				if (entityNavigations.Count == 0)
				{
					if (stack.Count > 0)
					{
						yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
					}
				}
				else
				{
					foreach (var navigation in entityNavigations)
					{
						var inverseNavigation = navigation.Inverse;
						if (inverseNavigation != null)
						{
							includedNavigations.Add(inverseNavigation);
						}
					}
					stack.Push(entityNavigations.GetEnumerator());
				}
				while (stack.Count > 0 && !stack.Peek().MoveNext())
				{
					stack.Pop();
				}

				if (stack.Count == 0)
				{
					break;
				}

				entityType = stack.Peek().Current.TargetEntityType;
			}
		}
	}
}