using Schulcast.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Schulcast.Server.Models
{
	public class Post : Model
	{
		[Required, MaxLength(100)]
		public string Title { get; set; }

		[Required]
		public string Content { get; set; }
		public DateTime Published { get; set; }
		public DateTime LastUpdated { get; set; }
		public int MemberId { get; set; }
		public Member Member { get; set; }
	}
}