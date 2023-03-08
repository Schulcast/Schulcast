namespace Schulcast.Application.Content;

public class BlogService : CachedCrudService<Post>
{
	private readonly MemberService _memberService;
	public BlogService(IDatabase database, ICurrentUserService currentUserService, IMemoryCache cache, MemberService memberService) : base(database, currentUserService, cache)
	{
		_memberService = memberService;
	}

	public override Task<Post> Create(Post model)
	{
		model.Published = DateTime.Now;
		return base.Create(model);
	}

	public override async Task<Post> Update(Post model)
	{
		model.Member = null;
		model.LastUpdated = DateTime.UtcNow;
		return await base.Update(model);
	}
}