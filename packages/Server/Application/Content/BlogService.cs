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
		var authenticatedAccount = await _memberService.GetAuthenticated();

		if (model.MemberId != _currentUserService.Id && authenticatedAccount.Role != MemberRoles.Admin)
		{
			throw new UnauthorizedAccessException();
		}

		model.LastUpdated = DateTime.Now;
		return await base.Update(model);
	}
}