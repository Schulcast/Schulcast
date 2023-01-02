namespace Schulcast.Application.Members;

public class MemberService : CrudService<Member>
{
	public MemberService(IDatabase database, ICurrentUserService currentUserService) : base(database, currentUserService) { }

	public async Task<Member?> GetAuthenticatedOrDefault()
	{
		return _currentUserService.Id is null ? null : await Get(_currentUserService.Id.Value);
	}

	public async Task<Member> GetAuthenticated()
	{
		return _currentUserService.Id is null
			? throw new UnauthorizedAccessException()
			: await Get(_currentUserService.Id.Value);
	}

	public async Task<Member> GetByNickname(string Nickname)
	{
		return (await _database.Members.FirstAsync(m => m.Nickname == Nickname)).WithoutPassword();
	}

	public override async Task<IEnumerable<Member>> GetAll()
	{
		return (await base.GetAll()).WithoutPasswords().ExcludeSuperAdmin();
	}

	public override async Task<Member> Get(int id)
	{
		return (await base.Get(id)).WithoutPassword();
	}

	public override async Task<Member> Create(Member model)
	{
		return (await base.Create(model)).WithoutPassword();
	}

	public override async Task<Member> Update(Member model)
	{
		if (string.IsNullOrWhiteSpace(model.Password))
		{
			var existingMember = await Get(model.Id);
			model.Password = existingMember.Password;
		}
		return (await base.Update(model)).WithoutPassword();
	}

	public async Task<Member> Authenticate(string nickname, string password)
	{
		if (nickname is null)
		{
			throw new Exception("The username is not provided");
		}

		if (password is null)
		{
			throw new Exception("The password is not provided");
		}

		var member = await GetByNickname(nickname);

		if (member.Password?.ToLower() != Sha256.GetHash(password).ToLower())
		{
			throw new SecurityException("Incorrect Password");
		}

		member.Token = Token.IssueAccountAccess(TimeSpan.FromDays(30), member);
		return member.WithoutPassword();
	}

	public async Task<IEnumerable<Post>> GetMemberPosts(int id)
	{
		var member = await Get(id);
		return member.Posts;
	}
}
