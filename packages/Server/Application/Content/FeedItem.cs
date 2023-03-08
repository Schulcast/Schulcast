namespace Schulcast.Application.Content;

public enum FeedItemType { Article, Podcast, Video }

public class FeedItem
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "IDE1006", Justification = "This is an indicator for the client to be able to identify the type of the model")]
	public string __typeName__ => GetType().Name;

	public DateTime Date { get; set; }
	public string Title { get; set; } = null!;
	public FeedItemType Type { get; set; }
	public string ImageUrl { get; set; } = null!;
	public string Link { get; set; } = null!;
	public string[] Tags { get; set; } = Array.Empty<string>();
}