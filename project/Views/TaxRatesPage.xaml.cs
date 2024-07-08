using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace pospolsl2024.Views
{
    public partial class TaxRatesPage : ContentPage
    {
        private readonly PosDatabase _database;
        public ObservableCollection<TaxRate> TaxRates { get; private set; }

        public TaxRatesPage(PosDatabase database)
        {
            InitializeComponent();
            _database = database;
            LoadTaxRates();

            MessagingCenter.Subscribe<AddTaxRatePage, TaxRate>(this, "AddTaxRate", (sender, taxRate) =>
            {
                TaxRates.Add(taxRate);
            });

            MessagingCenter.Subscribe<EditTaxRatePage, TaxRate>(this, "EditTaxRate", (sender, updatedTaxRate) =>
            {
                var taxRate = TaxRates.FirstOrDefault(t => t.tax_id == updatedTaxRate.tax_id);
                if (taxRate != null)
                {
                    taxRate.tax_name = updatedTaxRate.tax_name;
                    taxRate.tax_rate = updatedTaxRate.tax_rate;
                }
            });
        }

        private async void OnAddTaxRateButtonClicked(object sender, EventArgs e)
        {
            var addTaxRatePage = new AddTaxRatePage(_database);
            await Navigation.PushModalAsync(addTaxRatePage);
        }

        private async void OnEditTaxRateButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var taxRate = button.BindingContext as TaxRate;

            var editTaxRatePage = new EditTaxRatePage(_database, taxRate);
            await Navigation.PushModalAsync(editTaxRatePage);
        }

        private async void OnDeleteTaxRateButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var taxRate = button.BindingContext as TaxRate;

            bool confirmed = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {taxRate.tax_name}?", "Yes", "No");
            if (confirmed)
            {
                await _database.DeleteItem(taxRate);
                TaxRates.Remove(taxRate);
            }
        }

        private async void LoadTaxRates()
        {
            var taxRatesList = await _database.GetAllItems<TaxRate>();
            TaxRates = new ObservableCollection<TaxRate>(taxRatesList);
            BindingContext = this;
        }
    }
}