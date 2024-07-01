using Microsoft.Maui.Controls;
using pospolsl2024.Data;
using pospolsl2024.Models;

namespace pospolsl2024.Views
{
    public partial class CategoryFormPage : ContentPage
    {
        private readonly PosDatabase database;
        private Category category; // To hold the selected or new category

        public CategoryFormPage(PosDatabase posDatabase) // Constructor for a new category
        {
            InitializeComponent();
            database = posDatabase;
            category = new Category(); // Create a new category object
            BindingContext = category;
        }

        public CategoryFormPage(Category existingCategory, PosDatabase posDatabase) // Constructor for an existing category
        {
            InitializeComponent();
            database = posDatabase;
            category = existingCategory;
            BindingContext = category;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Update category properties based on the form values
            // ...

            await database.SaveItem(category); // Use SaveItemWithChildrenAsync for related objects

            // Navigate back to the CategoriesPage
            await Shell.Current.GoToAsync(".."); // This will take you back to the previous page
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (category.category_id != 0) // Ensure the category exists
            {
                bool confirmDelete = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this category?", "Yes", "No");
                if (confirmDelete)
                {
                    await database.DeleteItem(category);
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
    }

}