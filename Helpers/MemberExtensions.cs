using Schulcast.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Schulcast.Server.Helpers
{
	public static class MemberExtensions
	{
		public static Member WithoutPassword(this Member user)
		{
			user.Password = null!;
			return user;
		}

		public static IEnumerable<Member> WithoutPasswords(this IEnumerable<Member> users)
		{
			return users.Select(x => x.WithoutPassword());
		}

		public static IEnumerable<Member> ExcludeSuperAdmin(this IEnumerable<Member> users)
		{
			return users.Where(user => user.Nickname != "admin");
		}
	}
}