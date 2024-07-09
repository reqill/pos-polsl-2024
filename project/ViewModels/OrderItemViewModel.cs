using pospolsl2024.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        private OrderItem _orderItem;
        private Order _selectedOrder;
        private Food _selectedFood;

        public OrderItemViewModel(OrderItem orderItem = null)
        {
            _orderItem = orderItem ?? new OrderItem();
            if (orderItem == null)
            {
                Id = 0;
            }

            // Set selected order and food if available
            SelectedOrder = _orderItem.Order;
            SelectedFood = _orderItem.Food;
        }

        public int Id
        {
            get => _orderItem.Id;
            set
            {
                _orderItem.Id = value;
                OnPropertyChanged();
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                _orderItem.Order = value;
                _orderItem.order_id = value?.order_id ?? 0; // Update the foreign key
                OnPropertyChanged();
            }
        }

        public Food SelectedFood
        {
            get => _selectedFood;
            set
            {
                _selectedFood = value;
                _orderItem.Food = value;
                _orderItem.food_id = value?.food_id ?? 0; // Update the foreign key
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _orderItem.quantity;
            set
            {
                _orderItem.quantity = value;
                OnPropertyChanged();
            }
        }

        // PropertyChanged event and method
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Conversion method
        public OrderItem ToOrderItem() => _orderItem;
    }
}
