using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;

namespace pospolsl2024.Views
{
    public partial class AddTaxRatePage : ContentPage
    {
        private readonly PosDatabase _database;

        public AddTaxRatePage(PosDatabase database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaxNameEntry.Text) || string.IsNullOrWhiteSpace(TaxRateEntry.Text) || !decimal.TryParse(TaxRateEntry.Text, out decimal taxRate))
            {
                await DisplayAlert("Validation Error", "Please fill in both the Tax Name and a valid Tax Rate.", "OK");
                return;
            }

            var newTaxRate = new TaxRate
            {
                tax_name = TaxNameEntry.Text,
                tax_rate = taxRate
            };

            await _database.SaveItem(newTaxRate);
            MessagingCenter.Send(this, "AddTaxRate", newTaxRate);
            await Navigation.PopModalAsync();
        }
    }
}