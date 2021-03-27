using Schulcast.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schulcast.Server.Models
{
	public struct MemberRoles
	{
		public const string Admin = "Admin";
		public const string Member = "Member";
	}
	public class Member : Model
	{
		[Required, MaxLength(50)]
		public string Nickname { get; set; } = null!;
		[MaxLength(50)]
		public string Role { get; set; } = MemberRoles.Member;
		[MaxLength(64)]
		public string? Password { get; set; }
		[NotMapped]
		public string? Token { get; set; }
		public int ImageId { get; set; }
		public List<MemberTask> Tasks { get; set; } = new();
		public List<MemberData> Data { get; set; } = new();
		public List<Post> Posts { get; set; } = new();
	}
}