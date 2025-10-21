using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.ComponentModel;
using BakeryMobileApp.Helper;

namespace Session6.ViewModels
{
    public class OrderVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int ID;
        ApiHelper api = new ApiHelper();   
        public ObservableCollection<BakeryMobileApp.Models.Transaction> SavedOrders = new();
        public ObservableCollection<BakeryMobileApp.Models.Transaction> Orders { get; set; } = new();
        public async Task GetUserOrders(int id) 
        {
            var orderList = await api.GetUserOrders(id);
            foreach (var order in orderList) 
            {
                Orders.Add(order);
                SavedOrders.Add(order);
            }
        }

        public void Filter(string search) 
        {
            Orders.Clear();
            string _operator = string.Empty;
            string _number = string.Empty;

            if (string.IsNullOrEmpty(search) || search == ".")
            {
                foreach (var order in SavedOrders)
                    Orders.Add(order);

                return;
            } else if(search.StartsWith(">=") || search.StartsWith("<=")) 
            {
                _operator = search[..2]; 
                _number = search[2..];
            } else if(search.StartsWith(">") || search.StartsWith("<")) 
            {
                _operator = search[..1];
                _number = search[1..];
            }
            else
            {
                _operator = "="; 
                _number = search;
            }

            if (!decimal.TryParse(_number, out var value))
                return;

            IEnumerable<BakeryMobileApp.Models.Transaction> filtered = _operator switch
            {
                ">=" => SavedOrders.Where(o => o.TotalValue >= value),
                "<=" => SavedOrders.Where(o => o.TotalValue <= value),
                ">" => SavedOrders.Where(o => o.TotalValue > value),
                "<" => SavedOrders.Where(o => o.TotalValue < value),
                "=" => SavedOrders.Where(o => o.TotalValue == value),
                _ => SavedOrders
            };
            foreach (var t in filtered) Orders.Add(t);
        }
    }
}
