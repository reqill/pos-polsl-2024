using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;

namespace pospolsl2024.Views
{
    public partial class EditTaxRatePage : ContentPage
    {
        private readonly PosDatabase _database;
        private readonly TaxRate _taxRate;

        public EditTaxRatePage(PosDatabase database, TaxRate taxRate)
        {
            InitializeComponent();
            _database = database;
            _taxRate = taxRate;
            BindingContext = _taxRate;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaxNameEntry.Text) || string.IsNullOrWhiteSpace(TaxRateEntry.Text) || !decimal.TryParse(TaxRateEntry.Text, out decimal taxRate))
            {
                await DisplayAlert("Validation Error", "Please fill in both the Tax Name and a valid Tax Rate.", "OK");
                return;
            }

            _taxRate.tax_name = TaxNameEntry.Text;
            _taxRate.tax_rate = taxRate;

            await _database.SaveItem(_taxRate);
            MessagingCenter.Send(this, "EditTaxRate", _taxRate);
            await Navigation.PopModalAsync();
        }
    }
}
