using pospolsl2024.Data;
using pospolsl2024.Data; // Replace with your actual namespace
using System.Collections.ObjectModel; 
using System.Diagnostics;
using pospolsl2024.Data; // Replace with your actual namespace
namespace pospolsl2024.Views;

public partial class CategoriesPage : ContentPage
{
    private readonly PosDatabase database;
    public ObservableCollection<Category> Categories { get; private set; } = new ObservableCollection<Category>();
    public partial class CategoriesPage : ContentPage
    public CategoriesPage(PosDatabase posDatabase)
    {
        InitializeComponent();
        database = posDatabase;
        BindingContext = this;
        LoadCategories();
    }
            InitializeComponent();
    private async void AddCategory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CategoriesForm(database)); 
    }

    private async void EditCategory(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var category = (Category)button.BindingContext;
                category_id = 4,
        await Navigation.PushAsync(new CategoriesForm(database, category));
    }

    private async void DeleteCategory(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var category = (Category)button.BindingContext;

        bool confirmDelete = await DisplayAlert("Confirm Delete",
                                $"Are you sure you want to delete '{category.category_name}'?",
                                "Yes", "No");
        if (confirmDelete)
        {
            await database.DeleteItem(category);
            Categories.Remove(category);
            Debug.WriteLine($"Category '{category.category_name}' deleted.");
        }
    }

    private async void LoadCategories()
    {
        try
        {
            var allCategories = await database.GetAllItems<Category>();
            Categories.Clear();
            foreach (var category in allCategories)
            {
                Categories.Add(category);
            }
            Debug.WriteLine("Categories loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load categories: {ex.Message}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadCategories();
            Categories = await database.GetAllItems<Category>();
            System.Diagnostics.Debug.WriteLine(Categories.Count);
            BindingContext = this;
        }
    }
}
