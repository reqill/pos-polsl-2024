using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace pospolsl2024.Views
{
    public partial class FoodsForm : ContentPage
    {
        private readonly PosDatabase database;
        private readonly FoodViewModel viewModel;

        public FoodsForm(PosDatabase db, Food existingFood = null)
        {
            InitializeComponent();
            database = db;
            viewModel = new FoodViewModel(existingFood);
            BindingContext = viewModel;
            LoadPickerDataAsync();
        }

        private async void LoadPickerDataAsync()
        {
            try
            {
                var categories = await database.GetAllItems<Category>();
                var allergens = await database.GetAllItems<Allergen>();
                var taxRates = await database.GetAllItems<TaxRate>();

                viewModel.Categories.Clear();
                viewModel.Allergens.Clear();
                viewModel.TaxRates.Clear();

                foreach (var category in categories)
                    viewModel.Categories.Add(category);
                foreach (var allergen in allergens)
                    viewModel.Allergens.Add(allergen);
                foreach (var taxRate in taxRates)
                    viewModel.TaxRates.Add(taxRate);

                viewModel.SelectedCategory = viewModel.Categories.FirstOrDefault(c => c.category_id == viewModel.ToFood().category_id);
                if (viewModel.ToFood().Allergens != null && viewModel.ToFood().Allergens.Any())
                {
                    viewModel.SelectedAllergen = viewModel.Allergens.FirstOrDefault(a => a.allergen_id == viewModel.ToFood().Allergens.First().allergen_id);
                }
                if (viewModel.ToFood().TaxRates != null && viewModel.ToFood().TaxRates.Any())
                {
                    viewModel.SelectedTaxRate = viewModel.TaxRates.FirstOrDefault(t => t.tax_id == viewModel.ToFood().TaxRates.First().tax_id);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to load data.", "OK");
            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.Validate())
            {
                var food = viewModel.ToFood();
                if (viewModel.FoodId == 0)
                    await database.AddItem(food);
                else
                    await database.UpdateItem(food);

                await Navigation.PopAsync();
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
