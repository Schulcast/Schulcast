using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schulcast.Server.Data;
using Schulcast.Server.Models;

namespace Schulcast.Server.Controllers
{
	[ApiController, Route("[controller]"), Authorize]
	public class TaskController : ControllerBase
	{
		public TaskController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet, Authorize(Roles = MemberRoles.Admin)]
		public IActionResult GetAll()
		{
			return Ok(UnitOfWork.TaskRepository.GetAll());
		}

		[HttpGet("{id}"), AllowAnonymous]
		public IActionResult Get([FromRoute] int id)
		{
			return Ok(UnitOfWork.TaskRepository.Get(id));
		}

		[HttpPost, Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Post([FromBody] Task task)
		{
			UnitOfWork.TaskRepository.Add(task);
			UnitOfWork.CommitChanges();
			return Ok(task);
		}

		[HttpPut("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Put([FromRoute] int id, [FromBody] Task task)
		{
			if (id != task.Id)
			{
				return BadRequest();
			}

			UnitOfWork.TaskRepository.Update(task);
			UnitOfWork.CommitChanges();
			return Ok(task);
		}

		[HttpDelete("{id}"), Authorize(Roles = MemberRoles.Admin)]
		public IActionResult Delete([FromRoute] int id)
		{
			UnitOfWork.TaskRepository.Delete(id);
			UnitOfWork.CommitChanges();
			return Ok();
		}
	}
}