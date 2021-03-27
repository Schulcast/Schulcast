using Microsoft.EntityFrameworkCore;
using Schulcast.Server.Data;
using Schulcast.Server.Exceptions;
using Schulcast.Server.Models;
using System.Linq;

namespace Schulcast.Server.Repositories
{
	public class MemberRepository : RepositoryBase<Member>
	{
		public MemberRepository(DatabaseContext database) : base(database) { }

		public Member GetByNickname(string Nickname)
		{
			return Find(m => m.Nickname == Nickname);
		}
	}
}