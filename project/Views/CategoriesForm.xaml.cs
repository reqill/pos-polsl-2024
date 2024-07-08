using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System.Diagnostics;

namespace pospolsl2024.Views;

public partial class CategoriesForm : ContentPage
{
    private readonly PosDatabase database;
    public CategoryViewModel CategoryViewModel { get; set; }
    private bool isNewCategory => CategoryViewModel.CategoryId == 0;

    public string Title => CategoryViewModel.CategoryId == 0 ? "Add Category" : "Edit Category";

    public CategoriesForm(PosDatabase posDatabase, Category existingCategory = null)
    {
        InitializeComponent();
        database = posDatabase;

        CategoryViewModel = new CategoryViewModel(existingCategory); 

        Debug.WriteLine(existingCategory != null ? $"Editing Category: {existingCategory.category_name}" : "Adding a new Category");

        BindingContext = this;
        Content.BindingContext = CategoryViewModel;
    }

    private async void SaveCategory(object sender, EventArgs e)
    {
        var category = CategoryViewModel.ToCategory();

       if (isNewCategory)
            {
              await database.AddItem(category);
            }
       else
            {
              await database.UpdateItem(category);
            }

        Debug.WriteLine("Category saved successfully");
        await Navigation.PopAsync();
    }

    private async void CancelForm(object sender, EventArgs e)
    {
        Debug.WriteLine("Category creation/editing canceled");
        await Navigation.PopAsync();
    }
}
