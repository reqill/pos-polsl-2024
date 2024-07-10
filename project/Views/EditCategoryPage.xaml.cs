using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;
using System.IO;

namespace pospolsl2024.Views
{
    public partial class EditCategoryPage : ContentPage
    {
        private readonly PosDatabase _database;
        private readonly Category _category;

        public EditCategoryPage(PosDatabase database, Category category)
        {
            InitializeComponent();
            _database = database;
            _category = category;
            BindingContext = _category;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryNameEntry.Text) || string.IsNullOrWhiteSpace(DescriptionEditor.Text))
            {
                await DisplayAlert("Validation Error", "Please fill in both the Category Name and Description.", "OK");
                return;
            }

            _category.category_name = CategoryNameEntry.Text;
            _category.description = DescriptionEditor.Text;

            await _database.SaveItem(_category);
            MessagingCenter.Send(this, "EditCategory", _category);
            await Navigation.PopModalAsync();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}