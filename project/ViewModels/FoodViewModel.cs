using pospolsl2024.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class FoodViewModel : INotifyPropertyChanged
    {
        private Food _food;
        private string _validationError;
        private Category _selectedCategory;
        private Allergen _selectedAllergen;
        private TaxRate _selectedTaxRate;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Allergen> Allergens { get; set; }
        public ObservableCollection<TaxRate> TaxRates { get; set; }

        public FoodViewModel(Food food = null)
        {
            _food = food ?? new Food();
            Categories = new ObservableCollection<Category>();
            Allergens = new ObservableCollection<Allergen>();
            TaxRates = new ObservableCollection<TaxRate>();
        }

        public int FoodId
        {
            get => _food.food_id;
            set
            {
                if (_food.food_id != value)
                {
                    _food.food_id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FoodName
        {
            get => _food.food_name;
            set
            {
                if (_food.food_name != value)
                {
                    _food.food_name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _food.description;
            set
            {
                if (_food.description != value)
                {
                    _food.description = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Price
        {
            get => _food.price;
            set
            {
                if (_food.price != value)
                {
                    _food.price = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    _food.category_id = value?.category_id ?? 0;
                    _food.Category = value;
                    OnPropertyChanged();
                }
            }
        }

        public Allergen SelectedAllergen
        {
            get => _selectedAllergen;
            set
            {
                if (_selectedAllergen != value)
                {
                    _selectedAllergen = value;
                    _food.Allergens = new List<Allergen> { value };
                    OnPropertyChanged();
                }
            }
        }

        public TaxRate SelectedTaxRate
        {
            get => _selectedTaxRate;
            set
            {
                if (_selectedTaxRate != value)
                {
                    _selectedTaxRate = value;
                    _food.TaxRates = new List<TaxRate> { value };
                    OnPropertyChanged();
                }
            }
        }

        public string ValidationError
        {
            get => _validationError;
            set
            {
                if (_validationError != value)
                {
                    _validationError = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Validate()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(FoodName))
            {
                ValidationError = "Food name is required.";
                isValid = false;
            }
            else if (Price <= 0)
            {
                ValidationError = "Price must be greater than zero.";
                isValid = false;
            }
            else
            {
                ValidationError = null;
            }
            return isValid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Food ToFood() => _food;
    }
}
