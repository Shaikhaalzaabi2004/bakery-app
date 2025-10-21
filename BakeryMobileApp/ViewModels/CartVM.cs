using BakeryMobileApp.Helper;
using BakeryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Session6.ViewModels
{
    internal class CartVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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

        private Customer _customer { get; set; } = new Customer();
        public Customer Customer 
        { 
            get => _customer;
            set 
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            } 
        }
        ApiHelper api = new ApiHelper();
        public ObservableCollection<CartItem> _cart { get; set; } = new();
        public ObservableCollection<CartItem> Cart 
        { 
            get => _cart;
            set 
            {
                _cart = value;
                OnPropertyChanged(nameof(Cart));
            } 
        }
        public async void LoadInformation()
        {
            await GetCustomer();
            GetCart();
            GetCartTotal();
        }

        public async Task GetCustomer() 
        {
            var userId = Preferences.Get($"@user", string.Empty);
            Customer = await api.GetCustomerById(int.Parse(userId));
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
        }
        public void GetCartTotal()
        {
            FinalPrice = 0;

            foreach (var item in Cart)
                FinalPrice += item.Price * item.Quantity;
        }

        public void RemoveItemFromCart(int id)
        {
            var itemToRemove = Cart.FirstOrDefault(p => p.ProductId == id);
            if (itemToRemove != null)
            {
                Cart.Remove(itemToRemove);
                SaveCart();
            }
        }
        public void DecreaseQuantity(int id)
        {
            var itemToDecrease = Cart.FirstOrDefault(p => p.ProductId == id);
            if (itemToDecrease.Quantity == 1)
            {
                Cart.Remove(itemToDecrease);
            }
            else 
            {
                itemToDecrease.Quantity--;
            }
            SaveCart();
        }

        private void SaveCart() 
        {
            var cartSerialized = JsonSerializer.Serialize(Cart);
            Preferences.Set("@Cart", cartSerialized);
            GetCartTotal();
        }

        public async Task<bool> Checkout() 
        {
            if(Customer.Balance < FinalPrice) 
                return false;

            foreach (var item in Cart) 
            {
                Transaction transaction = new Transaction() 
                {
                    CustomerId = Customer.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Date = DateTime.Now,
                };

                await api.PlaceOrder(transaction);
            }

            return true;
        }
    }
}
