using Microsoft.Extensions.Logging;
using ShortUrlTest.Services.LocalStorage;
using ShortUrlTest.Services.UrlService;
using ShortUrlTest.ViewModels;

namespace ShortUrlTest;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
		.UseMauiApp<App>()
		.ConfigureFonts(fonts =>
		                {
			                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IUrlDatabase>(new UrlDatabase(Path.Combine(FileSystem.AppDataDirectory, "urlDatabase.db")));
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<IUrlShortenerService, UrlShortenerService>();

		return builder.Build();
	}
}