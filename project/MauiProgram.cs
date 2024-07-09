using Microsoft.Extensions.DependencyInjection.Extensions;
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
		builder.Services.AddTransient<DatabaseTablesPage>();
		builder.Services.AddTransient<EmployeesPage>();
		builder.Services.AddTransient<EmployeesForm>();
		builder.Services.AddTransient<FoodsPage>();
		builder.Services.AddTransient<AllergensPage>();
		builder.Services.AddTransient<AllergensForm>();
		builder.Services.AddTransient<TaxRatesPage>();
		builder.Services.AddTransient<TaxRatesForm>();
        builder.Services.AddSingleton<PosDatabase>();
		builder.Services.AddTransient<PickTheEmployee>();
		builder.Services.AddTransient<OrdersPage>();

		return builder.Build();
	}
}
