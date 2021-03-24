using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]"), Authorize]
	public class SlideController : ControllerBase
	{
		public SlideController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet, AllowAnonymous]
		public IActionResult Get()
		{
			return Ok(UnitOfWork.SlideRepository.GetAll());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public IActionResult Get([FromRoute] int id)
		{
			return Ok(UnitOfWork.SlideRepository.Get(id));
		}

		[HttpPost, Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Post([FromBody] Slide slide)
		{
			UnitOfWork.SlideRepository.Add(slide);
			UnitOfWork.CommitChanges();
			return Ok(slide);
		}

		[HttpPut("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Put([FromRoute] int id, [FromBody] Slide slide)
		{
			if (id != slide.Id)
			{
				return BadRequest();
			}

			UnitOfWork.SlideRepository.Update(slide);
			UnitOfWork.CommitChanges();
			return Ok(slide);
		}

		[HttpDelete("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.SlideRepository.Delete(id);
			return Ok();
		}
	}
}