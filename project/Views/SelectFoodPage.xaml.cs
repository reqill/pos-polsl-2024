using pospolsl2024.Data;
using pospolsl2024.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace pospolsl2024.Views
{
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        private int _quantity;
        public OrderItem OrderItem { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                }
            }
        }

        public OrderItemViewModel(OrderItem orderItem)
        {
            OrderItem = orderItem;
            _quantity = orderItem.quantity;
        }
    }

    public partial class SelectFoodPage : ContentPage
    {
        private readonly PosDatabase _database;
        private readonly Employee _employee;
        private readonly Order _order;
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Food> Foods { get; set; }
        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; }
        private decimal _totalPrice;

        public event EventHandler<Order> OrderAccepted;

        public SelectFoodPage(Employee employee, Order order = null)
        {
            InitializeComponent();
            _database = new PosDatabase();
            _employee = employee;
            _order = order;
            OrderItems = new ObservableCollection<OrderItemViewModel>();
            OrderSummaryCollectionView.ItemsSource = OrderItems;
            LoadCategories();
            _totalPrice = 0;
            if (_order != null)
            {
                LoadOrderItems(_order);
            }
            UpdateTotalPrice();
        }

        private async void LoadCategories()
        {
            Categories = new ObservableCollection<Category>(await _database.GetAllItems<Category>());
            CategoryPicker.ItemsSource = Categories;
        }

        private async void LoadOrderItems(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var food = await GetFoodByIdAsync(orderItem.food_id);
                if (food != null)
                {
                    orderItem.Food = food;
                    OrderItems.Add(new OrderItemViewModel(orderItem));
                    _totalPrice += orderItem.Food.price * orderItem.quantity;
                }
            }
            UpdateTotalPrice();
        }

        private async Task<Food> GetFoodByIdAsync(int foodId)
        {
            var foods = await _database.GetAllItems<Food>();
            return foods.FirstOrDefault(f => f.food_id == foodId);
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
                var existingOrderItem = OrderItems.FirstOrDefault(oi => oi.OrderItem.Food.food_id == selectedFood.food_id);
                if (existingOrderItem != null)
                {
                    existingOrderItem.Quantity++;
                }
                else
                {
                    var orderItem = new OrderItem { Food = selectedFood, quantity = 1 };
                    OrderItems.Add(new OrderItemViewModel(orderItem));
                }
                _totalPrice += selectedFood.price;
                UpdateTotalPrice();
            }
        }

        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is OrderItemViewModel orderItemViewModel)
            {
                orderItemViewModel.Quantity++;
                orderItemViewModel.OrderItem.quantity = orderItemViewModel.Quantity;
                _totalPrice += orderItemViewModel.OrderItem.Food.price;
                UpdateTotalPrice();
            }
        }

        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is OrderItemViewModel orderItemViewModel)
            {
                orderItemViewModel.Quantity--;
                orderItemViewModel.OrderItem.quantity = orderItemViewModel.Quantity;
                _totalPrice -= orderItemViewModel.OrderItem.Food.price;
                if (orderItemViewModel.Quantity == 0)
                {
                    OrderItems.Remove(orderItemViewModel);
                }
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

            var order = _order ?? new Order
            {
                employee_id = _employee.employee_id,
                Employee = _employee,
                order_date = DateTime.Now,
                status = "Accepted",
            };

            order.total_price = _totalPrice;
            order.OrderItems = OrderItems.Select(oi => oi.OrderItem).ToList();

            if (_order == null)
            {
                await _database.AddItem(order, true);
            }
            else
            {
                await _database.UpdateItem(order);
            }

            OrderAccepted?.Invoke(this, order);
            await Navigation.PopModalAsync();
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
