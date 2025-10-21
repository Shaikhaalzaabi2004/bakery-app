using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryMobileApp.Models
{
    public class CartItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PhotoName { get; set; }
        public double Price { get; set; }
        private int _quantity { get; set; } = 1;
        public int Quantity 
        { 
            get => _quantity;
            set 
            { 
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            } 
        }
    }
}
