namespace Schulcast.Application.Files;

public struct FileDirectories
{
	public const string Slides = "slides";
	public const string Members = "members";
	public const string Misc = "misc";
}

public class File : Model
{
	public string Path { get; set; } = null!;
}