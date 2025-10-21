using BakeryMobileApp.Models;

namespace Session6.ViewModels;

public partial class CartPage : ContentPage
{
    CartVM vm = new CartVM();
	public CartPage()
	{
		InitializeComponent();
        BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.LoadInformation();
    }
    private void removeItem_Tapped(object sender, TappedEventArgs e)
    {
        var labelObj = sender as Label;
        var itemToRemove = labelObj.BindingContext as CartItem;
        if (itemToRemove != null)
            vm.RemoveItemFromCart(itemToRemove.ProductId);
    }

    private void decrease_Tapped(object sender, TappedEventArgs e)
    {
        var labelObj = sender as Label;
        var itemToDecrease = labelObj.BindingContext as CartItem;
        if (itemToDecrease != null)
            vm.DecreaseQuantity(itemToDecrease.ProductId);
    }

    private async void checkoutBtn_Clicked(object sender, EventArgs e)
    {
        var result = await vm.Checkout();
        if (!result) 
        {
            DisplayAlert("Error", "Insufficient Funds", "Return");
        }
        else 
        { 
            DisplayAlert("Success", "Order Placed", "Return");
            Preferences.Set("@Cart", string.Empty);
            await Shell.Current.GoToAsync("ProductCatalog");
        }
    }
}