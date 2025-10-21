using BakeryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace Session6.ViewModels
{
    public class DetailsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int _quantity { get; set; } = 1;
        public int Quantity 
        { 
            get => _quantity;
            set 
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public double _finalPrice { get; set; } = 0;
        public double FinalPrice 
        { 
            get => _finalPrice;
            set 
            {
                _finalPrice = value;
                OnPropertyChanged(nameof(FinalPrice));
            }
        }
        public int _itemsInCart { get; set; } = 0;
        public int ItemsInCart 
        { 
            get => _itemsInCart;
            set 
            {
                _itemsInCart = value;
                OnPropertyChanged(nameof(ItemsInCart));
            }
        }

        ObservableCollection<CartItem> Cart = new();
        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product)); 
            }
        }
        public DetailsVM()
        {
            GetProduct();
            GetCart();
            GetCartTotal();
        }

        private void GetProduct() 
        {
            var productSerialized = Preferences.Get("@product", string.Empty);
            if (!string.IsNullOrEmpty(productSerialized)) 
                Product = JsonSerializer.Deserialize<Product>(productSerialized);
        }
        private void GetCart() 
        {
            Cart.Clear();
            var cartJson = Preferences.Get("@Cart", string.Empty);
            if (string.IsNullOrEmpty(cartJson)) 
            {
                Cart = new ObservableCollection<CartItem>();
            }
            else 
            {
                Cart = JsonSerializer.Deserialize<ObservableCollection<CartItem>>(cartJson);
            }

            ItemsInCart = Cart.Count();
        }
        public void GetCartTotal() 
        {
            FinalPrice = 0;

            foreach (var item in Cart)
                FinalPrice += item.Price * item.Quantity;
        }

        public void AddItemToCart() 
        {
            CartItem item = new CartItem() 
            {
                Price = (double)Product.Price,
                Quantity = Quantity,
                ProductId = Product.Id,
                ProductName = Product.Name,
                PhotoName = Product.PhotoName
            };

            var duplicate = Cart.FirstOrDefault(p => p.ProductId == Product.Id);
            if (duplicate != null) 
            {
                duplicate.Quantity += Quantity;
            }
            else 
            {
                Cart.Add(item);
            }
            var cartSerialized = JsonSerializer.Serialize(Cart);
            Preferences.Set("@Cart", cartSerialized);
            GetCart();
            GetCartTotal();
        }
    }
}
