using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;
using System.Collections.Generic;
using System.IO;

namespace pospolsl2024.Views
{
    public partial class CategoriesPage : ContentPage
    {
        private readonly PosDatabase database;
        public List<Category> Categories { get; private set; }

        public CategoriesPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
            LoadCategories();

            MessagingCenter.Subscribe<AddCategoryPage, Category>(this, "AddCategory", (sender, category) =>
            {
                Categories.Add(category);
                LoadCategories();
            });
        }

        private async void OnAddCategoryButtonClicked(object sender, EventArgs e)
        {
            var addCategoryPage = new AddCategoryPage(database);
            await Navigation.PushModalAsync(addCategoryPage);
        }

        private async void LoadCategories()
        {
            Categories = await database.GetAllItems<Category>();
            System.Diagnostics.Debug.WriteLine(Categories.Count);
            BindingContext = this;
        }
    }
}
