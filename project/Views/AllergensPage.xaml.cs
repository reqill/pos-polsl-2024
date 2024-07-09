using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace pospolsl2024.Views
{
    public partial class AllergensPage : ContentPage
    {
        private readonly PosDatabase database;
        public ObservableCollection<Allergen> Allergens { get; private set; } = new ObservableCollection<Allergen>();

        public AllergensPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
            BindingContext = this;
            LoadAllergens();
        }

        private async void AddAllergen(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllergensForm(database));
        }

        private async void EditAllergen(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var allergen = (Allergen)button.BindingContext;

            await Navigation.PushAsync(new AllergensForm(database, allergen));
        }

        private async void DeleteAllergen(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var allergen = (Allergen)button.BindingContext;

            bool confirmDelete = await DisplayAlert("Confirm Delete",
                                                    $"Are you sure you want to delete '{allergen.allergen_name}'?",
                                                    "Yes", "No");
            if (confirmDelete)
            {
                await database.DeleteItem(allergen);
                Allergens.Remove(allergen);
                Debug.WriteLine($"Allergen '{allergen.allergen_name}' deleted.");
            }
        }

        private async void LoadAllergens()
        {
            try
            {
                var allAllergens = await database.GetAllItems<Allergen>();
                Allergens.Clear();
                foreach (var allergen in allAllergens)
                {
                    Allergens.Add(allergen);
                }
                Debug.WriteLine("Allergens loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load allergens: {ex.Message}");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadAllergens();
        }
    }
}
