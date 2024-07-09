using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace pospolsl2024.Views;

public partial class EmployeesPage : ContentPage
{
    private readonly PosDatabase database;
    public ObservableCollection<Employee> Employees { get; private set; } = new ObservableCollection<Employee>();

    public EmployeesPage(PosDatabase posDatabase)
    {
        InitializeComponent();
        database = posDatabase;
        BindingContext = this;
        LoadEmployees();
    }

    private async void AddEmployee(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmployeesForm(database));
    }

    private async void EditEmployee(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var employee = (Employee)button.BindingContext;

        await Navigation.PushAsync(new EmployeesForm(database, employee));
    }

    private async void DeleteEmployee(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var employee = (Employee)button.BindingContext;

        bool confirmDelete = await DisplayAlert("Confirm Delete",
                                $"Are you sure you want to delete '{employee.name}'?",
                                "Yes", "No");
        if (confirmDelete)
        {
            await database.DeleteItem(employee);
            Employees.Remove(employee);
            Debug.WriteLine($"Employee '{employee.name}' deleted.");
        }
    }

    private async void LoadEmployees()
    {
        try
        {
            var allEmployees = await database.GetAllItems<Employee>();
            Employees.Clear();
            foreach (var employee in allEmployees)
            {
                Employees.Add(employee);
            }
            Debug.WriteLine("Employees loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load employees: {ex.Message}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadEmployees();
    }

}