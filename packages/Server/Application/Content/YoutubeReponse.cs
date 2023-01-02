namespace Schulcast.Application.Content;

public class PageInfo
{
	public int TotalResults { get; set; }
	public int ResultsPerPage { get; set; }
}

public class Id
{
	public string Kind { get; set; } = null!;
	public string VideoId { get; set; } = null!;
	public string PlaylistId { get; set; } = null!;
	public string ChannelId { get; set; } = null!;
}

public class Default
{
	public string Url { get; set; } = null!;
	public int Width { get; set; }
	public int Height { get; set; }
}

public class Medium
{
	public string Url { get; set; } = null!;
	public int Width { get; set; }
	public int Height { get; set; }
}

public class High
{
	public string Url { get; set; } = null!;
	public int Width { get; set; }
	public int Height { get; set; }
}

public class Thumbnails
{
	public Default Default { get; set; } = null!;
	public Medium Medium { get; set; } = null!;
	public High High { get; set; } = null!;
}

public class Snippet
{
	public DateTime PublishedAt { get; set; }
	public string ChannelId { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Thumbnails Thumbnails { get; set; } = null!;
	public string ChannelTitle { get; set; } = null!;
	public string LiveBroadcastContent { get; set; } = null!;
}

public class YoutubeItem
{
	public string Kind { get; set; } = null!;
	public string Etag { get; set; } = null!;
	public Id Id { get; set; } = null!;
	public Snippet Snippet { get; set; } = null!;
}

public class YoutubeResponse
{
	public string Kind { get; set; } = null!;
	public string Etag { get; set; } = null!;
	public string RegionCode { get; set; } = null!;
	public PageInfo PageInfo { get; set; } = null!;
	public List<YoutubeItem> Items { get; set; } = null!;
}