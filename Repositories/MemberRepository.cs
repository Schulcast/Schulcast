using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Data;
using Schulcast.Server.Exceptions;
using Schulcast.Server.Helpers;
using Schulcast.Server.Models;

namespace Schulcast.Server.Repositories
{
	public class MemberRepository : RepositoryBase<Member>
	{
		public MemberRepository(DatabaseContext database) : base(database) { }

		public Member GetByNickname(string Nickname)
		{
			var member = database.Members.Include(m => m.Data).Include(m => m.Tasks).ThenInclude(t => t.Task).FirstOrDefault(m => m.Nickname == Nickname);
			if (member is null)
				throw new EntityNotFoundException("Member was not found");
			return member;
		}
	}
}