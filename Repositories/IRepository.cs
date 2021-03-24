using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Schulcast.Server.Repositories
{
	public interface IRepository<T> where T : Model
	{
		bool Exists(int id);
		bool Exists(Expression<Func<T, bool>> predicate);
		T Find(Expression<Func<T, bool>> predicate);
		IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);
		T Get(int id);
		IEnumerable<T> GetAll();
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
		void Add(T entity);
		void AddRange(IEnumerable<T> entity);
		void Update(T entity);
		void UpdateRange(IEnumerable<T> entities);
	}
}
