using System.Linq;
using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Data;

namespace Schulcast.Server.Helpers
{
	public static class RepositoryExtensions
	{
		public static IQueryable<T> Eager<T>(this DatabaseContext context) where T : class
		{
			var query = context.Set<T>().AsQueryable();

			var navigations = context.Model.FindEntityType(typeof(T))
				.GetDerivedTypesInclusive()
				.SelectMany(type => type.GetNavigations())
				.Distinct();

			foreach (var property in navigations)
			{
				query = query.Include(property.Name);
			}

			return query;
		}
	}
}