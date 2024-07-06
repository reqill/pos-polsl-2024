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

        private async void AddCategory(object sender, EventArgs e)
        {
            Category diaryCategory = new Category
            {
                category_name = "Sides",
                category_id = 4,
                description = "Potatoes, Rice and all that stuff",
                photo = File.ReadAllBytes("C:\\Users\\amzee\\Source\\Repos\\reqill\\pos-polsl-2024\\project\\Resources\\Images\\sides.jpg")
            };

            await database.SaveItem(diaryCategory);
            Categories.Add(diaryCategory);
        }

        private async void LoadCategories()
        {
            Categories = await database.GetAllItems<Category>();
            System.Diagnostics.Debug.WriteLine(Categories.Count);
            BindingContext = this;
        }
    }
}
