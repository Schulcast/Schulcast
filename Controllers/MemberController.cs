using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Helpers;
using Schulcast.Server.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]"), Authorize]
	public class MemberController : ControllerBase
	{
		public MemberController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet("authenticate"), AllowAnonymous]
		public ActionResult<Member> Authenticate([FromQuery] string nickname, [FromQuery] string password)
		{
			if (nickname is null)
			{
				return BadRequest("The username is not provided");
			}

			if (password is null)
			{
				return BadRequest("The password is not provided");
			}

			var member = UnitOfWork.MemberRepository.GetByNickname(nickname);

			if (member.Password?.ToLower() != Sha256.GetHash(password).ToLower())
			{
				throw new AuthenticationException("Incorrect Password");
			}

			member.Token = Token.IssueAccountAccess(TimeSpan.FromDays(30), member);
			return Ok(member.WithoutPassword());
		}

		[HttpGet("{id}/blog"), AllowAnonymous]
		public ActionResult<IEnumerable<Post>> GetMemberPosts(int id)
		{
			var member = UnitOfWork.MemberRepository.Get(id);
			return Ok(member.Posts);
		}

		[HttpGet("{id}/image"), AllowAnonymous]
		public FileStreamResult GetMemberImage(int id)
		{
			var member = UnitOfWork.MemberRepository.Get(id);
			var filePath = UnitOfWork.FileRepository.Get(member.ImageId).Path;
			var file = System.IO.File.OpenRead(filePath);
			return File(file, "image/jpeg");
		}

		[HttpGet, AllowAnonymous]
		public ActionResult<IEnumerable<Member>> GetAll()
		{
			return Ok(UnitOfWork.MemberRepository.GetAll().ExcludeSuperAdmin().WithoutPasswords());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public ActionResult<Member> Get(int id)
		{
			return Ok(UnitOfWork.MemberRepository.Get(id).WithoutPassword());
		}

		[HttpPost, Authorize(Roles = MemberRoles.Admin)]
		public ActionResult<Member> Post([FromBody] Member member)
		{
			UnitOfWork.MemberRepository.Add(member);
			UnitOfWork.CommitChanges();
			return Ok(member.WithoutPassword());
		}

		[HttpPut("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public ActionResult<Member> Put([FromRoute] int id, [FromBody] Member member)
		{
			if (id != member.Id)
			{
				return BadRequest();
			}

			if (string.IsNullOrWhiteSpace(member.Password))
			{
				var existingMember = UnitOfWork.MemberRepository.Get(member.Id);
				member.Password = existingMember.Password;
			}

			UnitOfWork.MemberRepository.Update(member);
			UnitOfWork.CommitChanges();
			return Ok(member.WithoutPassword());
		}

		[HttpDelete("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public ActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.MemberRepository.Delete(id);
			UnitOfWork.CommitChanges();
			return Ok();
		}
	}
}