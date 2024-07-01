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

        private async void OnNewCategoryClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoryFormPage)}"); 
        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.FirstOrDefault() is Category selectedCategory)
            {
                // Pass the selected category object
                await Shell.Current.GoToAsync($"{nameof(CategoryFormPage)}?categoryId={selectedCategory.category_id}");
            }
        }
    }
}
