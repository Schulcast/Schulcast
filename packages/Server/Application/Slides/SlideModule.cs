namespace Schulcast.Application.Slides;

public class SlideModule : Module
{
	public override void ConfigureServices(IServiceCollection services) => services.AddTransient<SlideService>();

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/slide", [AllowAnonymous] (SlideService slideService) => slideService.GetAll());
		endpoints.MapGet("/api/slide/{id}", [AllowAnonymous] (SlideService slideService, int id) => slideService.Get(id));
		endpoints.MapPost("/api/slide", [Authorize(Roles = MemberRoles.Admin)] (SlideService slideService, Slide slide) => slideService.Create(slide));
		endpoints.MapPut("/api/slide", [Authorize(Roles = MemberRoles.Admin)] (SlideService slideService, Slide slide) => slideService.Update(slide));
		endpoints.MapDelete("/api/slide/{id}", [Authorize(Roles = MemberRoles.Admin)] (SlideService slideService, int id) => slideService.Delete(id));
	}
}