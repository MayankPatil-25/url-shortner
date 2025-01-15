using SQLite;

namespace ShortUrlTest.Models;

public class UrlItem
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string OriginalUrl { get; set; }
	public string ShortenedUrl { get; set; }
	
}
