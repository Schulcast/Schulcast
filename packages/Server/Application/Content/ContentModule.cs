namespace Schulcast.Application.Content;

public class ContentModule : Module
{
	public const string youtubeEndpoint = "https://www.googleapis.com/youtube/v3/search?key=AIzaSyDACSbBULNEdyDfEoTHt3_8VIeqCuSTaxI&channelId=UCtow4J8YJPGjppfiBnmHicQ&part=snippet,id&order=date&maxResults=20";
	public const string podcastEndpoint = "http://podcast.schulcast.de/feed.xml";

	private static (string? Value, DateTime? Date) YoutubeCache { get; set; } = (null, DateTime.MinValue);
	private static (string? Value, DateTime? Date) PodcastCache { get; set; } = (null, DateTime.MinValue);
	private static readonly TimeSpan cacheThreshold = TimeSpan.FromMinutes(15);

	public override void ConfigureServices(IServiceCollection services) => services.AddTransient<BlogService>();

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/blog", [AllowAnonymous] (BlogService blogService) => blogService.GetAll());
		endpoints.MapGet("/api/blog/{id}", [AllowAnonymous] (BlogService blogService, int id) => blogService.Get(id));
		endpoints.MapPost("/api/blog", (BlogService blogService, Post post) => blogService.Create(post));
		endpoints.MapPut("/api/blog", (BlogService blogService, Post post) => blogService.Update(post));
		endpoints.MapDelete("/api/blog/{id}", [Authorize(Roles = MemberRoles.Admin)] (BlogService blogService, int id) => blogService.Delete(id));
		endpoints.MapGet("/api/feed", [AllowAnonymous] async (BlogService blogService) =>
		{
			static async Task<YoutubeResponse> FetchVideos()
			{
				if (DateTime.Now - YoutubeCache.Date > cacheThreshold)
				{
					YoutubeCache = (await new WebClient().DownloadStringTaskAsync(youtubeEndpoint), DateTime.Now);
				}

				return JsonConvert.DeserializeObject<YoutubeResponse>(YoutubeCache.Value!)!;
			}

			static async Task<PodcastResponse?> FetchPodcasts()
			{
				try
				{
					if (DateTime.Now - PodcastCache.Date > cacheThreshold)
					{
						PodcastCache = (await new WebClient().DownloadStringTaskAsync(podcastEndpoint), DateTime.Now);
					}

					return PodcastCache.Value!.XmlDeserializeFromString<PodcastResponse>();
				}
				catch
				{
					return null;
				}
			}

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
				.Union((await podcastsTask)?.Channel.Item
					.Select(episode => new FeedItem
					{
						Title = episode.Title,
						ImageUrl = "http://podcast.schulcast.de/images/itunes_image.jpg",
						Date = Convert.ToDateTime(episode.PubDate),
						Type = FeedItemType.Podcast,
						Link = episode.Enclosure.Url
					}
				) ?? Enumerable.Empty<FeedItem>())
				.Union((await blogService.GetAll())
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

			return results;
		});
	}
}