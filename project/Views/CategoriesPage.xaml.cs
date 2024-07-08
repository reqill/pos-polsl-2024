using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace pospolsl2024.Views
{
    public partial class CategoriesPage : ContentPage
    {
        private readonly PosDatabase database;
        public ObservableCollection<Category> Categories { get; private set; }

        public CategoriesPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
            LoadCategories();

            MessagingCenter.Subscribe<AddCategoryPage, Category>(this, "AddCategory", (sender, category) =>
            {
                Categories.Add(category);
            });
            MessagingCenter.Subscribe<EditCategoryPage, Category>(this, "EditCategory", (sender, updatedCategory) =>
            {
                var category = Categories.FirstOrDefault(c => c.category_id == updatedCategory.category_id);
                if (category != null)
                {
                    category.category_name = updatedCategory.category_name;
                    category.description = updatedCategory.description;
                }
            });
        }

        private async void OnAddCategoryButtonClicked(object sender, EventArgs e)
        {
            var addCategoryPage = new AddCategoryPage(database);
            await Navigation.PushModalAsync(addCategoryPage);
        }

        private async void LoadCategories()
        {
            var categoriesList = await database.GetAllItems<Category>();
            Categories = new ObservableCollection<Category>(categoriesList);
            BindingContext = this;
        }

        private async void OnDeleteCategoryButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button.BindingContext as Category;

            bool confirmed = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {category.category_name}?", "Yes", "No");
            if (confirmed)
            {
                await database.DeleteItem(category);
                Categories.Remove(category);
            }
        }
        private async void OnEditCategoryButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button.BindingContext as Category;

            var editCategoryPage = new EditCategoryPage(database, category);
            await Navigation.PushModalAsync(editCategoryPage);
        }
    }
}
