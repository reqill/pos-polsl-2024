using pospolsl2024.Views;

namespace pospolsl2024;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
        Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
    }
}
