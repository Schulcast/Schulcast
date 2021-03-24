using System;
using System.Collections.Generic;

namespace Schulcast.Server.Models
{
	public class PageInfo
	{
		public int TotalResults { get; set; }
		public int ResultsPerPage { get; set; }
	}

	public class Id
	{
		public string Kind { get; set; }
		public string VideoId { get; set; }
		public string PlaylistId { get; set; }
		public string ChannelId { get; set; }
	}

	public class Default
	{
		public string Url { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class Medium
	{
		public string Url { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class High
	{
		public string Url { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}

	public class Thumbnails
	{
		public Default Default { get; set; }
		public Medium Medium { get; set; }
		public High High { get; set; }
	}

	public class Snippet
	{
		public DateTime PublishedAt { get; set; }
		public string ChannelId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public Thumbnails Thumbnails { get; set; }
		public string ChannelTitle { get; set; }
		public string LiveBroadcastContent { get; set; }
	}

	public class YoutubeItem
	{
		public string Kind { get; set; }
		public string Etag { get; set; }
		public Id Id { get; set; }
		public Snippet Snippet { get; set; }
	}

	public class YoutubeResponse
	{
		public string Kind { get; set; }
		public string Etag { get; set; }
		public string RegionCode { get; set; }
		public PageInfo PageInfo { get; set; }
		public List<YoutubeItem> Items { get; set; }
	}
}