namespace pospolsl2024.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnEditDatabaseButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DatabaseTablesPage));
        }
        private async void OnPOSButtonClicked(object sender, EventArgs e)
        {
            
        }
    }
}
