using Session6.ViewModels;

namespace Session6
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("ProductCatalog", typeof(ProductCatalog));
            Routing.RegisterRoute("ProductDetails", typeof(ProductDetails));
            Routing.RegisterRoute("Orders", typeof(Orders));
            Routing.RegisterRoute("FavoritesPage", typeof(FavoritesPage));
            Routing.RegisterRoute("CartPage", typeof(CartPage));
        }
    }
}
