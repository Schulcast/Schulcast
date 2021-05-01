using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Models;
using System.Collections.Generic;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]"), Authorize]
	public class SlideController : ControllerBase
	{
		public SlideController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet, AllowAnonymous]
		public ActionResult<IEnumerable<Slide>> Get()
		{
			return Ok(UnitOfWork.SlideRepository.GetAll());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public ActionResult<Slide> Get([FromRoute] int id)
		{
			return Ok(UnitOfWork.SlideRepository.Get(id));
		}

		[HttpPost, Authorize(Roles = MemberRoles.Admin)]
		public ActionResult<Slide> Post([FromBody] Slide slide)
		{
			UnitOfWork.SlideRepository.Add(slide);
			UnitOfWork.CommitChanges();
			return Ok(slide);
		}

		[HttpPut("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public ActionResult<Slide> Put([FromRoute] int id, [FromBody] Slide slide)
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
		public ActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.SlideRepository.Delete(id);
			return Ok();
		}
	}
}