using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Data;
using Schulcast.Server.Exceptions;
using Schulcast.Server.Helpers;

namespace Schulcast.Server.Repositories
{
	public abstract class RepositoryBase<T> : IRepository<T> where T : Model, new()
	{
		protected readonly DatabaseContext database;
		protected IQueryable<T> EagerTable => database.Eager<T>();
		protected DbSet<T> Table => database.Set<T>();
		public RepositoryBase(DatabaseContext database) => this.database = database;

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
			return (Table.Where(predicate).Count() != 0)
				? new List<T>()
				: EagerTable.Where(predicate).ToList();
		}

		public T Find(Expression<Func<T, bool>> predicate)
		{
			var entity = EagerTable.SingleOrDefault(predicate);
			if (entity == null)
				throw new EntityNotFoundException($"{typeof(T).Name.Remove(typeof(T).Name.IndexOf('`'))} was not found");
			return entity;
		}

		public virtual IEnumerable<T> GetAll()
		{
			if (Table.Count() == 0)
				return new List<T>();
			return EagerTable.ToList();
		}

		public virtual T Get(int id)
		{
			var entity = Table.SingleOrDefault(e => e.Id == id);
			if (entity == null)
				throw new EntityNotFoundException($"{typeof(T).Name.Remove(typeof(T).Name.IndexOf('`'))} was not found");

			return entity;
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
			Table.Update(entity);
		}

		public virtual void UpdateRange(IEnumerable<T> entities)
		{
			Table.UpdateRange(entities);
		}
	}
}
