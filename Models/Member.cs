using Schulcast.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schulcast.Server.Models
{
	public struct MemberRoles
	{
		public const string Admin = "Admin";
		public const string Member = "Member";
	}
	public class Member : Model
	{
		[Required]
		[MaxLength(50)]
		public string Nickname { get; set; }

		[MaxLength(50)]
		public string Role { get; set; }

		[MaxLength(64)]
		public string Password { get; set; }
		public int ImageId { get; set; }
		public List<MemberTask> Tasks { get; set; } = new List<MemberTask>();
		public List<MemberData> Data { get; set; } = new List<MemberData>();
		public List<Post> Posts { get; set; } = new List<Post>();
	}
}