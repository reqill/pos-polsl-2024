﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using pospolsl2024.Data;
using pospolsl2024.Views;

namespace pospolsl2024;

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

		builder.Services.AddTransient<CategoriesForm>();
        builder.Services.AddTransient<CategoriesPage>();
		builder.Services.AddTransient<ItemsPage>();
        builder.Services.AddSingleton<PosDatabase>();

		return builder.Build();
	}
}
