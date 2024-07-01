using Microsoft.Maui.Controls;
using pospolsl2024.Data; // Replace with your actual namespace
using pospolsl2024.Models;

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
        }

        private async void LoadCategories()
        {
            Categories = await database.GetAllItems<Category>(); 
            BindingContext = this;
        }
    }
}
