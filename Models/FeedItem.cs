using System;

namespace Schulcast.Server.Models
{
	public enum FeedItemType { Article, Podcast, Video }

	public class FeedItem
	{
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public FeedItemType Type { get; set; }
		public string ImageUrl { get; set; }
		public string Link { get; set; }
	}
}