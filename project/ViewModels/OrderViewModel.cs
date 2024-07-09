using pospolsl2024.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private Order _order;
        private ObservableCollection<OrderItem> _orderItems;
        private Employee _selectedEmployee;

        public OrderViewModel(Order order = null)
        {
            _order = order ?? new Order { order_date = DateTime.Now }; // Initialize with current time for new orders
            _orderItems = new ObservableCollection<OrderItem>(_order.OrderItems ?? new List<OrderItem>());
            SelectedEmployee = _order.Employee; // Set selected employee if available
        }

        public int OrderId
        {
            get => _order.order_id;
            set
            {
                _order.order_id = value;
                OnPropertyChanged();
            }
        }

        public DateTime OrderDate
        {
            get => _order.order_date;
            set
            {
                _order.order_date = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalPrice
        {
            get => _order.total_price;
            set
            {
                _order.total_price = value;
                OnPropertyChanged();
            }
        }

        public decimal Tip
        {
            get => _order.tip;
            set
            {
                _order.tip = value;
                OnPropertyChanged();
            }
        }

        public decimal NetPrice
        {
            get => _order.net_price;
            set
            {
                _order.net_price = value;
                OnPropertyChanged();
            }
        }

        public decimal TaxPrice
        {
            get => _order.tax_price;
            set
            {
                _order.tax_price = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get => _order.status;
            set
            {
                _order.status = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderItem> OrderItems
        {
            get => _orderItems;
            set
            {
                _orderItems = value;
                _order.OrderItems = value.ToList(); // Update the underlying Order's OrderItems
                OnPropertyChanged();
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                _order.Employee = value;
                _order.employee_id = value?.employee_id ?? 0; // Update the foreign key
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
        public Order ToOrder() => _order;
    }
}
