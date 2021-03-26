using Schulcast.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Schulcast.Server.Models
{
	public class Slide : Model
	{
		[Required, MaxLength(200)]
		public string Description { get; set; } = null!;
		public int ImageId { get; set; }
	}
}