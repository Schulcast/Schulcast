using System.Collections.Generic;
using System.Linq;
using Schulcast.Server.Models;

namespace Schulcast.Server.Helpers
{
	public static class ExtensionMethods
	{
		public static Member WithoutPassword(this Member user)
		{
			if (user is null)
				return null;

			user.Password = null;
			return user;
		}

		public static IEnumerable<Member> WithoutPasswords(this IEnumerable<Member> users)
		{
			return (users is not null)
				? users.Select(x => x.WithoutPassword())
				: null;
		}
	}
}