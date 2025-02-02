﻿using pospolsl2024.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pospolsl2024.ViewModels
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private Category _category;
        private string _validationError;

        public CategoryViewModel(Category category = null)
        {
            _category = category ?? new Category();
        }

        public int CategoryId
        {
            get => _category.category_id;
            set
            {
                _category.category_id = value;
                OnPropertyChanged();
            }
        }

        public string CategoryName
        {
            get => _category.category_name;
            set
            {
                _category.category_name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _category.description;
            set
            {
                _category.description = value;
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
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                ValidationError = "Category Name is required.";
                return false;
            }

            ValidationError = null;
            return true;
        }

        public Category ToCategory() => _category;
    }
}
