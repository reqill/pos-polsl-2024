using pospolsl2024.Data;
using pospolsl2024.Models;

namespace pospolsl2024.Views
{
    public partial class PickTheEmployee : ContentPage
    {
        private readonly PosDatabase _database;

        public List<Employee> Employees { get; set; }

        public PickTheEmployee()
        {
            InitializeComponent();
            _database = new PosDatabase();
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            Employees = await _database.GetAllItems<Employee>();
            EmployeeCollectionView.ItemsSource = Employees;
        }

        private async void OnEmployeeSelected(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.CommandParameter is Employee selectedEmployee)
            {
                await Navigation.PushAsync(new OrdersPage(selectedEmployee));
            }
        }
    }
}