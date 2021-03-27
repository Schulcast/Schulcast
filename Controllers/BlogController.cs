using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Models;
using System;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]")]
	public class BlogController : ControllerBase
	{
		public BlogController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet, AllowAnonymous]
		public IActionResult GetAll()
		{
			return Ok(UnitOfWork.BlogRepository.GetAll());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public IActionResult Get([FromRoute] int id)
		{
			return Ok(UnitOfWork.BlogRepository.Get(id));
		}

		[HttpDelete("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.BlogRepository.Delete(id);
			UnitOfWork.CommitChanges();
			return Ok();
		}

		[HttpPost]
		public IActionResult Post([FromBody] Post post)
		{
			post.Published = DateTime.Now;
			UnitOfWork.BlogRepository.Add(post);
			UnitOfWork.CommitChanges();
			return Ok(post);
		}

		[HttpPut("{id}")]
		public IActionResult Put([FromRoute] int id, [FromBody] Post post)
		{
			var authenticatedAccount = UnitOfWork.MemberRepository.Get(AuthenticatedAccountId);

			if (post.MemberId != AuthenticatedAccountId && authenticatedAccount.Role != MemberRoles.Admin)
			{
				return Forbid();
			}

			if (id != post.Id)
			{
				return BadRequest();
			}

			post.LastUpdated = DateTime.Now;
			UnitOfWork.BlogRepository.Update(post);
			UnitOfWork.CommitChanges();
			return Ok(post);
		}
	}
}