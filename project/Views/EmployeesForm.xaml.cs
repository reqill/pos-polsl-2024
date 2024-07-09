using pospolsl2024.Data;
using pospolsl2024.Models;
using pospolsl2024.ViewModels;
using System.Diagnostics;

namespace pospolsl2024.Views;

public partial class EmployeesForm : ContentPage
{
    private readonly PosDatabase database;
    public EmployeeViewModel EmployeeViewModel { get; set; }
    private bool isNewEmployee => EmployeeViewModel.EmployeeId == 0;

    public string Title => EmployeeViewModel.EmployeeId == 0 ? "Add Employee" : "Edit Employee";

    public EmployeesForm(PosDatabase posDatabase, Employee existingEmployee = null)
    {
        InitializeComponent();
        database = posDatabase;

        EmployeeViewModel = new EmployeeViewModel(existingEmployee);

        Debug.WriteLine(existingEmployee != null ? $"Editing Employee: {existingEmployee.name}" : "Adding a new Employee");

        BindingContext = this;
        Content.BindingContext = EmployeeViewModel;
    }

    private async void SaveEmployee(object sender, EventArgs e)
    {
        if (!EmployeeViewModel.Validate())
        {
            return;
        }

        var employee = EmployeeViewModel.ToEmployee();

        if (isNewEmployee)
        {
            await database.AddItem(employee);
        }
        else
        {
            await database.UpdateItem(employee);
        }

        Debug.WriteLine("Employee saved successfully");
        await Navigation.PopAsync();
    }

    private async void CancelForm(object sender, EventArgs e)
    {
        Debug.WriteLine("Employee creation/editing canceled");
        await Navigation.PopAsync();
    }
}
