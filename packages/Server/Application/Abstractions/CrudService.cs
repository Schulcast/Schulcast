namespace Schulcast.Application.Abstractions;

public abstract class CrudService<TModel> : Service where TModel : Model
{
	public CrudService(IDatabase database, ICurrentUserService currentUserService) : base(database, currentUserService) { }

	public virtual async Task<bool> Exists(int id)
		=> await _database.GetSet<TModel>().FindAsync(id) != null;

	public virtual async Task<TModel> Get(int id)
		=> await _database.GetSet<TModel>().IncludeAll().FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();

	public virtual async Task<TModel> GetSingle(int id)
		=> await _database.GetSet<TModel>().FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();

	public virtual async Task<IEnumerable<TModel>> GetAll()
		=> await _database.GetSet<TModel>().IncludeAll().AsNoTracking().ToArrayAsync();

	public virtual async Task<TModel> Create(TModel model)
	{
		_database.GetSet<TModel>().Add(model);
		await _database.CommitChangesAsync();
		return model;
	}

	public virtual async Task<TModel> Update(TModel model)
	{
		_database.GetSet<TModel>().Update(model);
		await _database.CommitChangesAsync();
		return model;
	}

	public virtual async Task<TModel> Delete(int id)
	{
		var model = await GetSingle(id);
		_database.GetSet<TModel>().Remove(model);
		await _database.CommitChangesAsync();
		return model;
	}
}