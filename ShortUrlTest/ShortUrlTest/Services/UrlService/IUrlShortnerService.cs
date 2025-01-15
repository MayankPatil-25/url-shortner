namespace ShortUrlTest.Services.UrlService;

public interface IUrlShortenerService
{
	string ShortenUrl(string originalUrl);
}