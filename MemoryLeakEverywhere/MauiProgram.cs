using MemoryLeakEverywhere.ViewModels;
using MemoryLeakEverywhere.Views;
using Microsoft.Extensions.Logging;

namespace MemoryLeakEverywhere;

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

        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<MainViewModel>();        
		
		builder.Services.AddScoped<CollectionViewSamplePage>();
        builder.Services.AddScoped<CollectionViewSampleViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
