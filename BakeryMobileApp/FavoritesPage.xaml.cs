using BakeryMobileApp.Models;
using Session6.ViewModels;
using System.Text.Json;

namespace Session6;

public partial class FavoritesPage : ContentPage
{
    FavoritesVM? vm;
    string searchFilter = string.Empty;
    List<Category> categoryFilter = new List<Category>();
    public FavoritesPage()
	{
		InitializeComponent();

        var idString = Preferences.Get("@user", string.Empty);
        if (!string.IsNullOrEmpty(idString))
            vm = new FavoritesVM(int.Parse(idString));

        BindingContext = vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (vm != null)
            await vm.LoadInformation();
    }

    private void categoryBtn_Clicked(object sender, EventArgs e)
    {
        categoriesStack.IsVisible = true;

    }

    private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        searchFilter = searchEntry.Text;
        vm.Filter(categoryFilter, searchFilter);
    }

    private void categoryCb_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkboxObj = sender as CheckBox;
        var category = checkboxObj.BindingContext as Category;
        if (category != null)
        {
            if (categoryFilter.Contains(category))
            {
                categoryFilter.Remove(category);
            }
            else
            {
                categoryFilter.Add(category);
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        categoriesStack.IsVisible = false;
        vm.Filter(categoryFilter, searchFilter);
    }

    private void unfavorite_Tapped(object sender, TappedEventArgs e)
    {
        var imageObj = sender as Image;
        var productToUnfavorite = imageObj.BindingContext as Product;
        if (productToUnfavorite != null)
        {
            vm.RemoveProductFromFavorites(productToUnfavorite);
        }
    }

    private async void details_Tapped(object sender, TappedEventArgs e)
    {
        var textObj = sender as Label;
        var product = textObj.BindingContext as Product;
        if (product != null)
        {
            var serializedProduct = JsonSerializer.Serialize(product);
            Preferences.Set("@product", serializedProduct);
            await Shell.Current.GoToAsync("ProductDetails");
        }
    }
}