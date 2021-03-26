using Schulcast.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Schulcast.Server.Models
{
	public class Task : Model
	{
		[Required, MaxLength(50)]
		public string Title { get; set; } = null!;
		public List<MemberTask> Members { get; set; } = new();
	}
}