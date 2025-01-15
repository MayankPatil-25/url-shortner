using ShortUrlTest.Models;

namespace ShortUrlTest.Services.LocalStorage;

public interface IUrlDatabase
{
	Task<List<UrlItem>> GetUrlsAsync();
	Task AddUrlAsync(UrlItem urlItem);
	Task DeleteUrlAsync(UrlItem id);
}