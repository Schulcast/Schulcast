using Schulcast.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Schulcast.Server.Models
{
	public class MemberData : Model
	{
		[Required, MaxLength(100)]
		public string Title { get; set; }

		[Required]
		public string Response { get; set; }

		[Required]
		public int MemberId { get; set; }
		public Member Member { get; set; }
	}
}