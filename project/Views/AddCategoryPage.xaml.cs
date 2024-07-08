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
            var newCategory = new Category
            {
                category_name = CategoryNameEntry.Text,
                description = DescriptionEditor.Text,
                // Assuming you have a way to set the photo
                // photo = ... 
            };

            await database.SaveItem(newCategory);
            MessagingCenter.Send(this, "AddCategory", newCategory);
            await Navigation.PopModalAsync();
        }
    }
}