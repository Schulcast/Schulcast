namespace Schulcast;

public static class EnvironmentVariables
{
	public static string JWT_SECRET => Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new ArgumentNullException();
	public static string BASE_URL => Environment.GetEnvironmentVariable("BASE_URL") is string url && !string.IsNullOrWhiteSpace(url) ? url : "https://schulcast.de";
}