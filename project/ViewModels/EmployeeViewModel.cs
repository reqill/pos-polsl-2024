using pospolsl2024.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _employee;
        private string _validationError;

        public EmployeeViewModel(Employee employee = null)
        {
            _employee = employee ?? new Employee();
        }

        public int EmployeeId
        {
            get => _employee.employee_id;
            set
            {
                _employee.employee_id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _employee.name;
            set
            {
                _employee.name = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get => _employee.position;
            set
            {
                _employee.position = value;
                OnPropertyChanged();
            }
        }

        public string ValidationError
        {
            get => _validationError;
            set
            {
                _validationError = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Employee ToEmployee() => _employee;

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ValidationError = "Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Position))
            {
                ValidationError = "Position is required.";
                return false;
            }

            ValidationError = null;
            return true;
        }
    }
}
