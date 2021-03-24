using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Schulcast.Server.Data;
using Schulcast.Server.Helpers;
using Schulcast.Server.Models;

namespace Schulcast.Server.Controllers
{
	public class YoutubeApiStorage
	{
		public string LastResponse { get; set; }
		public static DateTime LastResponseDate { get; set; }
	}

	[ApiController, Route("[controller]")]
	public class FeedController : ControllerBase
	{
		private const string youtubeEndpoint = "https://www.googleapis.com/youtube/v3/search?key=AIzaSyDACSbBULNEdyDfEoTHt3_8VIeqCuSTaxI&channelId=UCtow4J8YJPGjppfiBnmHicQ&part=snippet,id&order=date&maxResults=20";
		private const string podcastEndpoint = "http://podcast.schulcast.de/feed.php";

		public FeedController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet]
		public IActionResult Get()
		{
			var list = new List<FeedItem>();
			var wc = new WebClient();
			var database = new DatabaseContext();

			var podcastsXml = wc.DownloadString(podcastEndpoint);

			if (DateTime.Now - Program.YoutubeStorage.lastResponse > TimeSpan.FromMinutes(15))
			{
				Program.YoutubeStorage = (wc.DownloadString(youtubeEndpoint), DateTime.Now);
			}

			var videosJson = Program.YoutubeStorage.json;

			var videos = JsonConvert.DeserializeObject<YoutubeResponse>(videosJson);
			foreach (var video in videos.items)
			{
				if (video.id.kind == "youtube#video")
				{
					list.Add(new FeedItem()
					{
						Title = video.snippet.title,
						ImageUrl = video.snippet.thumbnails.high.url,
						Date = video.snippet.publishedAt,
						Type = FeedItemType.Video,
						Link = $"https://www.youtube.com/watch?v={video.id.videoId}"
					});
				}
			}

			var podcasts = Utilities.XmlDeserializeFromString<PodcastResponse>(podcastsXml);

			foreach (var episode in podcasts.Channel.Item)
			{
				list.Add(new FeedItem()
				{
					Title = episode.Title,
					ImageUrl = "http://podcast.schulcast.de/images/itunes_image.jpg",
					Date = Convert.ToDateTime(episode.PubDate),
					Type = FeedItemType.Podcast,
					Link = episode.Enclosure.Url
				});
			}

			foreach (var post in database.Posts)
			{
				list.Add(new FeedItem
				{
					Title = post.Title,
					ImageUrl = "",
					Date = post.Published,
					Type = FeedItemType.Article,
					Link = $"blog/{post.Id}"
				});
			}

			list = list.OrderByDescending(p => p.Date).ToList();

			return Ok(list);
		}
	}
}