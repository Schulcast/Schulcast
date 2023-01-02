namespace Schulcast.Application.Content;

[XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
public class Link
{
	[XmlAttribute(AttributeName = "href")]
	public string Href { get; set; } = null!;

	[XmlAttribute(AttributeName = "rel")]
	public string Rel { get; set; } = null!;

	[XmlAttribute(AttributeName = "type")]
	public string Type { get; set; } = null!;
}

[XmlRoot(ElementName = "image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
public class Image
{
	[XmlAttribute(AttributeName = "href")]
	public string Href { get; set; } = null!;
}

[XmlRoot(ElementName = "image")]
public class Image2
{
	[XmlElement(ElementName = "url")]
	public string Url { get; set; } = null!;

	[XmlElement(ElementName = "title")]
	public string Title { get; set; } = null!;

	[XmlElement(ElementName = "link")]
	public string Link2 { get; set; } = null!;
}

[XmlRoot(ElementName = "owner", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
public class Owner
{
	[XmlElement(ElementName = "name", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Name { get; set; } = null!;

	[XmlElement(ElementName = "email", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Email { get; set; } = null!;
}

[XmlRoot(ElementName = "category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
public class CategoryItem
{
	[XmlAttribute(AttributeName = "text")]
	public string Text { get; set; } = null!;

	[XmlElement(ElementName = "category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public CategoryItem Category { get; set; } = null!;
}

[XmlRoot(ElementName = "enclosure")]
public class Enclosure
{
	[XmlAttribute(AttributeName = "url")]
	public string Url { get; set; } = null!;

	[XmlAttribute(AttributeName = "length")]
	public string Length { get; set; } = null!;

	[XmlAttribute(AttributeName = "type")]
	public string Type { get; set; } = null!;
}

[XmlRoot(ElementName = "item")]
public class Episode
{
	[XmlElement(ElementName = "title")]
	public string Title { get; set; } = null!;

	[XmlElement(ElementName = "subtitle", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Subtitle { get; set; } = null!;

	[XmlElement(ElementName = "summary", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Summary { get; set; } = null!;

	[XmlElement(ElementName = "description")]
	public string Description { get; set; } = null!;

	[XmlElement(ElementName = "link")]
	public string Link2 { get; set; } = null!;

	[XmlElement(ElementName = "enclosure")]
	public Enclosure Enclosure { get; set; } = null!;

	[XmlElement(ElementName = "guid")]
	public string Guid { get; set; } = null!;

	[XmlElement(ElementName = "duration", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Duration { get; set; } = null!;

	[XmlElement(ElementName = "image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public Image Image { get; set; } = null!;

	[XmlElement(ElementName = "author")]
	public List<string> Author { get; set; } = null!;

	[XmlElement(ElementName = "keywords", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Keywords { get; set; } = null!;

	[XmlElement(ElementName = "explicit", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Explicit { get; set; } = null!;

	[XmlElement(ElementName = "pubDate")]
	public string PubDate { get; set; } = null!;
}

[XmlRoot(ElementName = "channel")]
public class Channel
{
	[XmlElement(ElementName = "title")]
	public string Title { get; set; } = null!;

	[XmlElement(ElementName = "link")]
	public List<string> Link { get; set; } = null!;

	[XmlElement(ElementName = "description")]
	public string Description { get; set; } = null!;

	[XmlElement(ElementName = "generator")]
	public string Generator { get; set; } = null!;

	[XmlElement(ElementName = "lastBuildDate")]
	public string LastBuildDate { get; set; } = null!;

	[XmlElement(ElementName = "language")]
	public string Language { get; set; } = null!;

	[XmlElement(ElementName = "copyright")]
	public string Copyright { get; set; } = null!;

	[XmlElement(ElementName = "image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public Image Image { get; set; } = null!;

	[XmlElement(ElementName = "image")]
	public Image2 Image2 { get; set; } = null!;

	[XmlElement(ElementName = "summary", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Summary { get; set; } = null!;

	[XmlElement(ElementName = "subtitle", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Subtitle { get; set; } = null!;

	[XmlElement(ElementName = "author", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Author { get; set; } = null!;

	[XmlElement(ElementName = "owner", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public Owner Owner { get; set; } = null!;

	[XmlElement(ElementName = "explicit", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public string Explicit { get; set; } = null!;

	[XmlElement(ElementName = "category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
	public List<CategoryItem> Category { get; set; } = null!;

	[XmlElement(ElementName = "item")]
	public List<Episode> Item { get; set; } = null!;
}

[XmlRoot(ElementName = "rss")]
public class PodcastResponse
{
	[XmlElement(ElementName = "channel")]
	public Channel Channel { get; set; } = null!;

	[XmlAttribute(AttributeName = "itunes", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Itunes { get; set; } = null!;

	[XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Atom { get; set; } = null!;

	[XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
	public string Lang { get; set; } = null!;

	[XmlAttribute(AttributeName = "version")]
	public string Version { get; set; } = null!;
}