using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;

namespace pospolsl2024.Views
{
    public partial class AddCategoryPage : ContentPage
    {
        private readonly PosDatabase database;

        public AddCategoryPage(PosDatabase posDatabase)
        {
            InitializeComponent();
            database = posDatabase;
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryNameEntry.Text) || string.IsNullOrWhiteSpace(DescriptionEditor.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in both the Category Name and Description.", "OK");
                return;
            }

            var newCategory = new Category
            {
                category_name = CategoryNameEntry.Text,
                description = DescriptionEditor.Text,
            };

            await database.SaveItem(newCategory);
            MessagingCenter.Send(this, "AddCategory", newCategory);
            await Navigation.PopModalAsync();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}