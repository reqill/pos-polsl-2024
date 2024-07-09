using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System;
using System.Diagnostics;

namespace pospolsl2024.Views
{
    public partial class TaxRatesForm : ContentPage
    {
        private readonly PosDatabase database;
        public TaxRateViewModel TaxRateViewModel { get; set; }
        private bool isNewTaxRate => TaxRateViewModel.TaxId == 0;

        public string Title => isNewTaxRate ? "Add Tax Rate" : "Edit Tax Rate";

        public TaxRatesForm(PosDatabase posDatabase, TaxRate existingTaxRate = null)
        {
            InitializeComponent();
            database = posDatabase;

            TaxRateViewModel = new TaxRateViewModel(existingTaxRate);
            Debug.WriteLine(existingTaxRate != null ? $"Editing Tax Rate: {existingTaxRate.tax_name}" : "Adding a new Tax Rate");

            BindingContext = this;
            Content.BindingContext = TaxRateViewModel;
        }

        private async void SaveTaxRate(object sender, EventArgs e)
        {
            if (!TaxRateViewModel.Validate())
                return;

            var taxRate = TaxRateViewModel.ToTaxRate();

            if (isNewTaxRate)
            {
                await database.AddItem(taxRate);
            }
            else
            {
                await database.UpdateItem(taxRate);
            }

            Debug.WriteLine("Tax Rate saved successfully");
            await Navigation.PopAsync();
        }

        private async void CancelForm(object sender, EventArgs e)
        {
            Debug.WriteLine("Tax Rate creation/editing canceled");
            await Navigation.PopAsync();
        }
    }
}
