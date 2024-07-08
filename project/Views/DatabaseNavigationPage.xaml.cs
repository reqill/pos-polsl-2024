using Microsoft.Maui.Controls;
using pospolsl2024.Data;
using pospolsl2024.Models;
using System;

namespace pospolsl2024.Views
{
    public partial class DatabaseNavigationPage : ContentPage
    {
        private readonly PosDatabase database;

        public DatabaseNavigationPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
        }

        private async void OnCategoriesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoriesPage(database));
        }

        private async void OnTaxRatesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaxRatesPage(database)); 
        }

        //
    }
}