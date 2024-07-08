using Microsoft.Maui.Controls;
using pospolsl2024.Models;
using pospolsl2024.Data;

namespace pospolsl2024.Views
{
    public partial class ItemsPage : ContentPage
    {
        private readonly PosDatabase database;
        public List<Food> Items { get; private set; }

        public ItemsPage(PosDatabase posDatabase, Category category)
        {
            InitializeComponent();
            database = posDatabase;
            
        }

        
    }
}