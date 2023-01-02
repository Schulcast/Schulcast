namespace Schulcast.Application.Slides;

public class SlideService : CachedCrudService<Slide>
{
	public SlideService(IDatabase database, ICurrentUserService currentUserService, IMemoryCache cache) : base(database, currentUserService, cache) { }
}
