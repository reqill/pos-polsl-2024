using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;

namespace pospolsl2024.Views
{
    public partial class SelectFoodPage : ContentPage
    {
        private readonly PosDatabase _database;
        private readonly Employee _employee;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; }
        private decimal _totalPrice;

        public event EventHandler<Order> OrderAccepted;

        public SelectFoodPage(Employee employee)
        {
            InitializeComponent();
            _database = new PosDatabase();
            _employee = employee;
            OrderItems = new ObservableCollection<OrderItem>();
            OrderSummaryCollectionView.ItemsSource = OrderItems;
            LoadCategories();
            _totalPrice = 0;
            UpdateTotalPrice();
        }

        private async void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(await _database.GetAllItems<Category>());
            CategoryPicker.ItemsSource = Categories;
        }

        private async void OnCategorySelected(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedItem is Category selectedCategory)
            {
                Foods = new ObservableCollection<Food>(await _database.GetAllItems<Food>(f => f.category_id == selectedCategory.category_id));
                FoodCollectionView.ItemsSource = Foods;
            }
        }

        private void OnAddToOrderClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Food selectedFood)
            {
                var orderItem = new OrderItem { Food = selectedFood, quantity = 1 };
                OrderItems.Add(orderItem);
                _totalPrice += selectedFood.price;
                UpdateTotalPrice();
            }
        }

        private async void OnAcceptOrderClicked(object sender, EventArgs e)
        {
            if (OrderItems.Count == 0)
            {
                await DisplayAlert("Error", "The order list is empty. Please add items to the order before accepting.", "OK");
                return;
            }

            var order = new Order
            {
                employee_id = _employee.employee_id,
                Employee = _employee,
                order_date = DateTime.Now,
                total_price = _totalPrice,
                status = "Accepted",
                OrderItems = OrderItems.ToList()
            };

            await _database.AddItem(order, true);
            OrderAccepted?.Invoke(this, order);
            await Navigation.PopModalAsync();
        }

        private void OnDeleteOrderItemClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is OrderItem orderItem)
            {
                OrderItems.Remove(orderItem);
                _totalPrice -= orderItem.Food.price;
                UpdateTotalPrice();
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void UpdateTotalPrice()
        {
            TotalPriceLabel.Text = $"Total Price: {_totalPrice:C}";
        }
    }
}