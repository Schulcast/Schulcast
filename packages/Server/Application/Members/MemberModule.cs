namespace Schulcast.Application.Members;

public class MemberModule : Module
{
	public override void ConfigureServices(IServiceCollection services) => services.AddTransient<MemberService>();

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/member", [AllowAnonymous] (MemberService memberService) => memberService.GetAll());
		endpoints.MapGet("/api/member/{id}", [AllowAnonymous] (MemberService memberService, int id) => memberService.Get(id));
		endpoints.MapPost("/api/member", [Authorize(Roles = MemberRoles.Admin)] (MemberService memberService, Member member) => memberService.Create(member));
		endpoints.MapPut("/api/member", [Authorize(Roles = MemberRoles.Admin)] (MemberService memberService, Member member) => memberService.Update(member));
		endpoints.MapDelete("/api/member/{id}", [Authorize(Roles = MemberRoles.Admin)] (MemberService memberService, int id) => memberService.Delete(id));
		endpoints.MapGet("/api/member/authenticate", [AllowAnonymous] (MemberService memberService, string nickname, string password) => memberService.Authenticate(nickname, password));
		endpoints.MapGet("/api/member/is-authenticated", [AllowAnonymous] (MemberService memberService) => memberService.IsAuthenticated());
		endpoints.MapGet("/api/member/{id}/blog", [AllowAnonymous] (MemberService memberService, int id) => memberService.GetMemberPosts(id));
		endpoints.MapGet("/api/member/{id}/image", [AllowAnonymous] async (MemberService memberService, FileService fileService, int id) =>
		{
			var member = await memberService.Get(id);
			var filePath = (await fileService.Get(member.ImageId)).Path;
			var file = System.IO.File.OpenRead(filePath);
			return Results.File(file, "image/jpeg");
		});
	}
}