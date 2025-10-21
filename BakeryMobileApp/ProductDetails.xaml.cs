using Session6.ViewModels;

namespace Session6;

public partial class ProductDetails : ContentPage
{
    DetailsVM vm = new DetailsVM();
    public ProductDetails()
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    private void addToCartBtn_Clicked(object sender, EventArgs e)
    {
        vm.AddItemToCart();
    }
}