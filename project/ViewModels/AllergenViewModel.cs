using pospolsl2024.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class AllergenViewModel : INotifyPropertyChanged
    {
        private Allergen _allergen;
        private string _validationError;

        public AllergenViewModel(Allergen allergen = null)
        {
            _allergen = allergen ?? new Allergen();
        }

        public int AllergenId
        {
            get => _allergen.allergen_id;
            set
            {
                _allergen.allergen_id = value;
                OnPropertyChanged();
            }
        }

        public string AllergenName
        {
            get => _allergen.allergen_name;
            set
            {
                _allergen.allergen_name = value;
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

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(AllergenName))
            {
                ValidationError = "Allergen Name is required.";
                return false;
            }

            ValidationError = null;
            return true;
        }

        public Allergen ToAllergen() => _allergen;
    }
}
