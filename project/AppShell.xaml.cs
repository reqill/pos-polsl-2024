using pospolsl2024.Views;

namespace pospolsl2024;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        Routing.RegisterRoute(nameof(DatabaseNavigationPage), typeof(DatabaseNavigationPage));
        Routing.RegisterRoute(nameof(TaxRatesPage), typeof(TaxRatesPage));
    }
}
