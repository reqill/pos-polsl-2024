using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel; // Import for observable collection
using System.Diagnostics;

namespace pospolsl2024.Views;

public partial class CategoriesPage : ContentPage
{
    private readonly PosDatabase database;
    public ObservableCollection<Category> Categories { get; private set; } = new ObservableCollection<Category>();

    public CategoriesPage(PosDatabase posDatabase)
    {
        InitializeComponent();
        database = posDatabase;
        BindingContext = this;
        LoadCategories();
    }

    private async void AddCategory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CategoriesForm(database)); 
    }

    private async void EditDeleteCategory(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var category = (Category)button.BindingContext;

        string action = await DisplayActionSheet("Choose Action", "Cancel", null, "Edit", "Delete");

        if (action == "Edit")
        {
            await Navigation.PushAsync(new CategoriesForm(database, category));
        }
        else if (action == "Delete")
        {
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
    }
}
