using Microsoft.EntityFrameworkCore;
using Schulcast.Core.Models;
using Schulcast.Server.Data;
using Schulcast.Server.Exceptions;
using Schulcast.Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Schulcast.Server.Repositories
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : Model, new()
	{
		protected readonly DatabaseContext database;
		protected DbSet<T> Table => database.Set<T>();
		protected IQueryable<T> EagerTable => database.Eager<T>();

		public RepositoryBase(DatabaseContext database)
		{
			this.database = database;
		}

		public bool Exists(int id)
		{
			return database.Set<T>().Find(id) != null;
		}

		public bool Exists(Expression<Func<T, bool>> predicate)
		{
			return Table.SingleOrDefault(predicate) != null;
		}

		public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
		{
			return Table.Where(predicate).Any()
				? new List<T>()
				: EagerTable.Where(predicate).ToList();
		}

		public T Find(Expression<Func<T, bool>> predicate)
		{
			var entity = EagerTable.SingleOrDefault(predicate);
			return entity ?? throw new EntityNotFoundException($"{typeof(T).Name.Remove(typeof(T).Name.IndexOf('`'))} was not found");
		}

		public virtual IEnumerable<T> GetAll()
		{
			return !Table.Any() ? new List<T>() : EagerTable.ToList();
		}

		public virtual T Get(int id)
		{
			var entity = Table.Find(id);
			return entity ?? throw new EntityNotFoundException($"{typeof(T).Name.Remove(typeof(T).Name.IndexOf('`'))} was not found");
		}

		public virtual void Delete(T entity)
		{
			Table.Remove(entity);
		}

		public virtual void Delete(int id)
		{
			Delete(new T { Id = id });
		}

		public virtual void DeleteRange(IEnumerable<T> entities)
		{
			Table.RemoveRange(entities);
		}

		public virtual void Add(T entity)
		{
			Table.Add(entity);
		}

		public virtual void AddRange(IEnumerable<T> entities)
		{
			Table.AddRange(entities);
		}

		public virtual void Update(T entity)
		{
			var foundEntity = Get(entity.Id);
			database.Entry(foundEntity).CurrentValues.SetValues(entity);
		}

		public virtual void UpdateRange(IEnumerable<T> entities)
		{
			var foundEntities = FindAll(f => entities.Select(e => e.Id).Contains(f.Id));
			foreach (var entity in foundEntities)
			{
				database.Entry(entity).CurrentValues.SetValues(entity);
			}
		}
	}
}