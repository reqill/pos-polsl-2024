namespace pospolsl2024.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCategoriesButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CategoriesPage));
        }
    }
}
