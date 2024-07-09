using pospolsl2024.Data;

namespace pospolsl2024.Views;

public partial class DatabaseTablesPage : ContentPage
{
    private readonly PosDatabase database;

    public DatabaseTablesPage(PosDatabase posDatabase)
    {
        InitializeComponent();
        database = posDatabase;
    }

    private async void OnCategoriesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new CategoriesPage(database));
    private async void OnEmployeesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new EmployeesPage(database)); 
    private async void OnFoodsClicked(object sender, EventArgs e) => await Navigation.PushAsync(new FoodsPage(database)); 
    private async void OnAllergensClicked(object sender, EventArgs e) => await Navigation.PushAsync(new AllergensPage(database));
    private async void OnTaxRatesClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TaxRatesPage(database));
}

