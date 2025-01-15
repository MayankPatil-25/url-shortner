using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShortUrlTest.Models;
using ShortUrlTest.Services.LocalStorage;

namespace ShortUrlTest.ViewModels;

public partial class MainViewModel : ObservableObject
{
	private readonly IUrlDatabase _urlDatabase;
	private const string copyToClipboardHint = "Copied to Clipboard";
	
	public ObservableCollection<UrlItem> UrlItems { get; } = new();

	[ObservableProperty]
	private string _originalUrl;

	[ObservableProperty]
	private string _shortenedUrl;
	
	public MainViewModel()
	{
		_urlDatabase = App.Services.GetService<IUrlDatabase>();
		LoadUrls();
		
	}

	private async void LoadUrls()
	{
		var urls = await _urlDatabase.GetUrlsAsync();
		UrlItems.Clear();
		foreach (var url in urls)
			UrlItems.Add(url);
	}

	[RelayCommand]
	public async void ShortenUrl()
	{
		if (string.IsNullOrWhiteSpace(OriginalUrl))
		{
			await App.Current.MainPage.DisplayAlert("Alert!", "The provided input is empty. Please try again with a valid url.", "Ok");
			return;
		}

		if (!IsValidUrl(OriginalUrl))
		{
			await App.Current.MainPage.DisplayAlert("Error!", "Unfortunately, The provided input is not a valid url. Please try again with a valid url.", "Ok");
			return;
		}

		ShortenedUrl = $"shortenedUrl.ly/{Guid.NewGuid().ToString("N")[..6]}";
			var urlItem = new UrlItem { OriginalUrl = OriginalUrl, ShortenedUrl = ShortenedUrl };
			await _urlDatabase.AddUrlAsync(urlItem);
			UrlItems.Add(urlItem);
			OriginalUrl = string.Empty;
		
	}

	[RelayCommand]
	public async void DeleteUrl(UrlItem urlItem)
	{
		await _urlDatabase.DeleteUrlAsync(urlItem);
		UrlItems.Remove(urlItem);
	}
	
	[RelayCommand]
	public async void CopyShortUrl(UrlItem urlItem)
	{
		await Clipboard.SetTextAsync(urlItem.ShortenedUrl);
		await App.Current.MainPage.DisplayAlert("Copied!", "The short url is copied to the clipboard.", "Ok");
	}

	public bool IsValidUrl(string url)
	{
		return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
		       && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
	}
}
