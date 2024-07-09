using pospolsl2024.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class TaxRateViewModel : INotifyPropertyChanged
    {
        private TaxRate _taxRate;
        private string _validationError;

        public TaxRateViewModel(TaxRate taxRate = null)
        {
            _taxRate = taxRate ?? new TaxRate();
        }

        public int TaxId
        {
            get => _taxRate.tax_id;
            set
            {
                _taxRate.tax_id = value;
                OnPropertyChanged();
            }
        }

        public string TaxName
        {
            get => _taxRate.tax_name;
            set
            {
                _taxRate.tax_name = value;
                OnPropertyChanged();
            }
        }

        public decimal TaxRateValue
        {
            get => _taxRate.tax_rate;
            set
            {
                _taxRate.tax_rate = value;
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(TaxName))
            {
                ValidationError = "Tax Name is required.";
                return false;
            }

            if (TaxRateValue <= 0 || TaxRateValue > 100)
            {
                ValidationError = "Please enter a valid tax rate (1-100%).";
                return false;
            }

            ValidationError = null;
            return true;
        }

        public TaxRate ToTaxRate() => _taxRate;
    }
}
