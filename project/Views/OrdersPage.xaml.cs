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
        private ObservableCollection<Order> _allOrders;

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
                Orders.Add(order);
            }

            _allOrders = new ObservableCollection<Order>(Orders);
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
            _allOrders.Add(order);
        }

        private async void OnRealizeOrderClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Order order)
            {
                order.status = "Done";
                await _database.UpdateItem(order); // Update the order status in the database
                OrdersCollectionView.ItemsSource = null; // Refresh the CollectionView
                OrdersCollectionView.ItemsSource = Orders;
            }
        }

        private async void OnEditOrderClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Order order)
            {
                var selectFoodPage = new SelectFoodPage(_employee, order);
                selectFoodPage.OrderAccepted += OnOrderEdited;
                await Navigation.PushModalAsync(selectFoodPage);
            }
        }

        private void OnOrderEdited(object sender, Order order)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.order_id == order.order_id);
            if (existingOrder != null)
            {
                Orders.Remove(existingOrder);
                Orders.Add(order);
            }
            _allOrders = new ObservableCollection<Order>(Orders);
        }

        private void OnStatusSelected(object sender, EventArgs e)
        {
            var selectedStatus = StatusPicker.SelectedItem.ToString();
            if (selectedStatus == "All")
            {
                Orders = new ObservableCollection<Order>(_allOrders);
            }
            else
            {
                Orders = new ObservableCollection<Order>(_allOrders.Where(o => o.status == selectedStatus));
            }
            OrdersCollectionView.ItemsSource = Orders;
        }
    }
}
