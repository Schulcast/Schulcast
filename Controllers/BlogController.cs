using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Models;
using System;
using System.Collections.Generic;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]")]
	public class BlogController : ControllerBase
	{
		public BlogController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet, AllowAnonymous]
		public ActionResult<IEnumerable<Post>> GetAll()
		{
			return Ok(UnitOfWork.BlogRepository.GetAll());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public ActionResult<Post> Get([FromRoute] int id)
		{
			return Ok(UnitOfWork.BlogRepository.Get(id));
		}

		[HttpDelete("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public ActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.BlogRepository.Delete(id);
			UnitOfWork.CommitChanges();
			return Ok();
		}

		[HttpPost]
		public ActionResult<Post> Post([FromBody] Post post)
		{
			post.Published = DateTime.Now;
			UnitOfWork.BlogRepository.Add(post);
			UnitOfWork.CommitChanges();
			return Ok(post);
		}

		[HttpPut("{id}")]
		public ActionResult<Post> Put([FromRoute] int id, [FromBody] Post post)
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