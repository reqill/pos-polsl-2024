using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;


namespace pospolsl2024.Views
{
    public partial class TaxRatesPage : ContentPage
    {
        private readonly PosDatabase database;
        public ObservableCollection<TaxRate> TaxRates { get; private set; } = new ObservableCollection<TaxRate>();

        public TaxRatesPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
            BindingContext = this;
            LoadTaxRates();
        }

        private async void AddTaxRate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaxRatesForm(database)); // Assuming a TaxRatesForm exists for adding/editing
        }

        private async void EditTaxRate(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var taxRate = (TaxRate)button.BindingContext;

            await Navigation.PushAsync(new TaxRatesForm(database, taxRate));
        }

        private async void DeleteTaxRate(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var taxRate = (TaxRate)button.BindingContext;

            bool confirmDelete = await DisplayAlert("Confirm Delete",
                                    $"Are you sure you want to delete this tax rate of {taxRate.tax_rate}?",
                                    "Yes", "No");
            if (confirmDelete)
            {
                await database.DeleteItem(taxRate);
                TaxRates.Remove(taxRate);
                Debug.WriteLine($"Tax rate of {taxRate.tax_rate} deleted.");
            }
        }

        private async void LoadTaxRates()
        {
            try
            {
                var allTaxRates = await database.GetAllItems<TaxRate>();
                TaxRates.Clear();
                foreach (var rate in allTaxRates)
                {
                    TaxRates.Add(rate);
                }
                Debug.WriteLine("Tax rates loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load tax rates: {ex.Message}");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTaxRates();
        }
    }
}
