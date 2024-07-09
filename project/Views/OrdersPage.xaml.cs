using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;

namespace pospolsl2024.Views
{
    public partial class OrdersPage : ContentPage
    {
        private readonly PosDatabase _database;
        private readonly Employee _employee;
        public ObservableCollection<Order> Orders { get; set; }

        public OrdersPage(Employee employee)
        {
            InitializeComponent();
            _database = new PosDatabase();
            _employee = employee;
            Orders = new ObservableCollection<Order>();
            OrdersCollectionView.ItemsSource = Orders;
            DisplayEmployee();
            LoadOrders();
        }

        private void DisplayEmployee()
        {
            EmployeeNameLabel.Text = _employee.name;
        }

        private async void LoadOrders()
        {
            var orders = await _database.GetAllItems<Order>(null, true);
            var employees = await _database.GetAllItems<Employee>();

            var employeeDictionary = employees.ToDictionary(e => e.employee_id);

            foreach (var order in orders)
            {
                if (employeeDictionary.TryGetValue(order.employee_id, out var employee))
                {
                    order.Employee = employee;
                }
                order.status = "Accepted";
                Orders.Add(order);
            }
        }

        private async void OnAddFoodClicked(object sender, EventArgs e)
        {
            var selectFoodPage = new SelectFoodPage(_employee);
            selectFoodPage.OrderAccepted += OnOrderAccepted;
            await Navigation.PushModalAsync(selectFoodPage);
        }

        private void OnOrderAccepted(object sender, Order order)
        {
            order.status = "Accepted";
            Orders.Add(order);
        }

        private async void OnDeleteOrderClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Order order)
            {
                Orders.Remove(order);
                await _database.DeleteItem(order, true);
            }
        }
    }
}
