namespace Schulcast.Application.Slides;

public class Slide : Model
{
	[Required, MaxLength(200)] public string Description { get; set; } = null!;
	public int ImageId { get; set; }
}