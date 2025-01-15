using ShortUrlTest.Models;
using SQLite;

namespace ShortUrlTest.Services.LocalStorage;

public class UrlDatabase : IUrlDatabase
{
	private readonly SQLiteAsyncConnection _database;

	public UrlDatabase(string dbPath)
	{
		_database = new SQLiteAsyncConnection(dbPath);
		_database.CreateTableAsync<UrlItem>().Wait();
	}

	public Task<List<UrlItem>> GetUrlsAsync() => _database.Table<UrlItem>().ToListAsync();
	public Task AddUrlAsync(UrlItem urlItem) => _database.InsertAsync(urlItem);
	public Task DeleteUrlAsync(UrlItem urlItem) => _database.DeleteAsync(urlItem);
}
