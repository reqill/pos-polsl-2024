using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System;
using System.Diagnostics;

namespace pospolsl2024.Views
{
    public partial class AllergensForm : ContentPage
    {
        private readonly PosDatabase database;
        public AllergenViewModel AllergenViewModel { get; set; }
        private bool isNewAllergen => AllergenViewModel.AllergenId == 0;

        public string Title => isNewAllergen ? "Add Allergen" : "Edit Allergen";

        public AllergensForm(PosDatabase posDatabase, Allergen existingAllergen = null)
        {
            InitializeComponent();
            database = posDatabase;

            AllergenViewModel = new AllergenViewModel(existingAllergen);

            Debug.WriteLine(existingAllergen != null ? $"Editing Allergen: {existingAllergen.allergen_name}" : "Adding a new Allergen");

            BindingContext = this;
            Content.BindingContext = AllergenViewModel;
        }

        private async void SaveAllergen(object sender, EventArgs e)
        {
            if (!AllergenViewModel.Validate())
                return;

            var allergen = AllergenViewModel.ToAllergen();

            if (isNewAllergen)
            {
                await database.AddItem(allergen);
            }
            else
            {
                await database.UpdateItem(allergen);
            }

            Debug.WriteLine("Allergen saved successfully");
            await Navigation.PopAsync();
        }

        private async void CancelForm(object sender, EventArgs e)
        {
            Debug.WriteLine("Allergen creation/editing canceled");
            await Navigation.PopAsync();
        }
    }
}
