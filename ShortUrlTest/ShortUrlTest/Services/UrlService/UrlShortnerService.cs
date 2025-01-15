namespace ShortUrlTest.Services.UrlService;

public class UrlShortenerService : IUrlShortenerService
{
	public string ShortenUrl(string originalUrl)
	{
		// Simple URL shortening logic
		return $"short.ly/{Guid.NewGuid().ToString("N")[..6]}";
	}
}
