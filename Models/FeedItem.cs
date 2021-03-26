using System;

namespace Schulcast.Server.Models
{
	public enum FeedItemType { Article, Podcast, Video }

	public class FeedItem
	{
		public DateTime Date { get; set; }
		public string Title { get; set; } = null!;
		public FeedItemType Type { get; set; }
		public string ImageUrl { get; set; } = null!;
		public string Link { get; set; } = null!;
	}
}