using pospolsl2024.Views;

namespace pospolsl2024;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(CategoriesForm), typeof(CategoriesForm));
        Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        Routing.RegisterRoute(nameof(DatabaseTablesPage), typeof(DatabaseTablesPage));
        Routing.RegisterRoute(nameof(EmployeesPage), typeof(EmployeesPage));
        Routing.RegisterRoute(nameof(EmployeesForm), typeof(EmployeesForm));
        Routing.RegisterRoute(nameof(FoodsPage), typeof(FoodsPage));
        Routing.ReferenceEquals(nameof(FoodsForm), typeof(FoodsForm));
        Routing.RegisterRoute(nameof(TaxRatesPage), typeof(TaxRatesPage));
        Routing.RegisterRoute(nameof(TaxRatesForm), typeof(TaxRatesForm));
        Routing.RegisterRoute(nameof(AllergensPage), typeof(AllergensPage));
        Routing.RegisterRoute(nameof(AllergensForm), typeof(AllergensForm));
        Routing.RegisterRoute(nameof(PickTheEmployee), typeof(PickTheEmployee));
        Routing.RegisterRoute(nameof(OrdersPage), typeof(OrdersPage));
    }
}
