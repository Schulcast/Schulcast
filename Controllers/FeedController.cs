using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Schulcast.Server.Data;
using Schulcast.Server.Helpers;
using Schulcast.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ControllerBase = Schulcast.Core.Controllers.ControllerBase;

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
		static (string Value, DateTime? Date) YoutubeCache { get; set; } = (null, DateTime.MinValue);
		static (string Value, DateTime? Date) PodcastCache { get; set; } = (null, DateTime.MinValue);
		static readonly TimeSpan cacheThreshold = TimeSpan.FromMinutes(15);

		const string youtubeEndpoint = "https://www.googleapis.com/youtube/v3/search?key=AIzaSyDACSbBULNEdyDfEoTHt3_8VIeqCuSTaxI&channelId=UCtow4J8YJPGjppfiBnmHicQ&part=snippet,id&order=date&maxResults=20";
		const string podcastEndpoint = "http://podcast.schulcast.de/feed.php";

		static async Task<YoutubeResponse> FetchVideos()
		{
			if (DateTime.Now - YoutubeCache.Date > cacheThreshold)
			{
				YoutubeCache = (await new WebClient().DownloadStringTaskAsync(youtubeEndpoint), DateTime.Now);
			}

			return JsonConvert.DeserializeObject<YoutubeResponse>(YoutubeCache.Value);
		}

		static async Task<PodcastResponse> FetchPodcasts()
		{
			if (DateTime.Now - PodcastCache.Date > cacheThreshold)
			{
				PodcastCache = (await new WebClient().DownloadStringTaskAsync(podcastEndpoint), DateTime.Now);
			}

			return Utilities.XmlDeserializeFromString<PodcastResponse>(PodcastCache.Value);
		}

		public FeedController(UnitOfWork unitOfWork) : base(unitOfWork) { }

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			// Start fetching videos and podcasts in parallel
			var videosTask = FetchVideos();
			var podcastsTask = FetchPodcasts();

			var results = new List<FeedItem>()
				.Union((await videosTask).Items
					.Where(video => video.Id.Kind == "youtube#video")
					.Select(video => new FeedItem
					{
						Title = video.Snippet.Title,
						ImageUrl = video.Snippet.Thumbnails.High.Url,
						Date = video.Snippet.PublishedAt,
						Type = FeedItemType.Video,
						Link = $"https://www.youtube.com/watch?v={video.Id.VideoId}"
					})
				)
				.Union((await podcastsTask).Channel.Item
					.Select(episode => new FeedItem
					{
						Title = episode.Title,
						ImageUrl = "http://podcast.schulcast.de/images/itunes_image.jpg",
						Date = Convert.ToDateTime(episode.PubDate),
						Type = FeedItemType.Podcast,
						Link = episode.Enclosure.Url
					})
				)
				.Union(UnitOfWork.BlogRepository
					.GetAll()
					.Select(post => new FeedItem
					{
						Title = post.Title,
						ImageUrl = "",
						Date = post.Published,
						Type = FeedItemType.Article,
						Link = $"blog/{post.Id}"
					})
				)
				.OrderByDescending(p => p.Date)
				.ToList();

			return Ok(results);
		}
	}
}