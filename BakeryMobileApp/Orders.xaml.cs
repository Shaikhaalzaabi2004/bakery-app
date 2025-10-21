using Session6.ViewModels;

namespace Session6;

public partial class Orders : ContentPage
{
	OrderVM vm = new OrderVM();
	public Orders()
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var userIdString = Preferences.Get("@user", string.Empty);

        if (!string.IsNullOrEmpty(userIdString))
        {
            vm.ID = int.Parse(userIdString);
            await vm.GetUserOrders(vm.ID);
        }

    }
    private async void homeBtn_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("ProductCatalog");
    }

    private void historySearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(historySearchBar.Text.StartsWith(">=") ||
            historySearchBar.Text.StartsWith("<=") ||
            historySearchBar.Text.StartsWith(">") ||
            historySearchBar.Text.StartsWith("<") ||
            historySearchBar.Text == "." ) 
        {
            vm.Filter(historySearchBar.Text);
        }
    }
}