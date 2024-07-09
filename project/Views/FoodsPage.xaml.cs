using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace pospolsl2024.Views;

public partial class FoodsPage : ContentPage
{
    private readonly PosDatabase database;
    public ObservableCollection<Food> Foods { get; private set; } = new ObservableCollection<Food>();

    public FoodsPage(PosDatabase posDatabase)
    {
        InitializeComponent();
        database = posDatabase;
        BindingContext = this;
        LoadFoods();
    }

    private async void AddFood(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FoodsForm(database));
    }

    private async void EditFood(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var food = (Food)button.BindingContext;
        await Navigation.PushAsync(new FoodsForm(database, food)); 
    }

    private async void DeleteFood(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var food = (Food)button.BindingContext;

        bool confirmDelete = await DisplayAlert("Confirm Delete",
                                $"Are you sure you want to delete '{food.food_name}'?",
                                "Yes", "No");
        if (confirmDelete)
        {
            await database.DeleteItem(food);
            Foods.Remove(food);
            Debug.WriteLine($"Food '{food.food_name}' deleted.");
        }
    }

    private async void LoadFoods()
    {
        try
        {
            var allFoods = await database.GetAllItems<Food>();
            Foods.Clear();
            foreach (var food in allFoods)
            {
                Foods.Add(food);
            }
            Debug.WriteLine("Foods loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load foods: {ex.Message}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFoods();
    }
}
