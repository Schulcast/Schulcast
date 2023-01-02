namespace Schulcast.Application.Abstractions;

public abstract class CachedCrudService<TModel> : CrudService<TModel> where TModel : Model
{
	protected virtual TimeSpan CacheDuration => TimeSpan.FromDays(1);

	private readonly IMemoryCache _cache;
	private string CacheKey => GetType().Name;

	public CachedCrudService(IDatabase database, ICurrentUserService currentUserService, IMemoryCache cache) : base(database, currentUserService)
		=> _cache = cache;

	public async Task<IEnumerable<TModel>> GetAllCached()
	{
		return await _cache.GetOrCreateAsync(CacheKey, async entry =>
		{
			entry.AbsoluteExpirationRelativeToNow = CacheDuration;
			return await GetAll();
		}) ?? Enumerable.Empty<TModel>();
	}

	public override async Task<TModel> Create(TModel model)
	{
		var result = await base.Create(model);
		RemoveCache();
		return result;
	}

	public override async Task<TModel> Update(TModel model)
	{
		var result = await base.Update(model);
		RemoveCache();
		return result;
	}

	public override async Task<TModel> Delete(int id)
	{
		var result = await base.Delete(id);
		RemoveCache();
		return result;
	}

	public void RemoveCache() => _cache.Remove(CacheKey);
}