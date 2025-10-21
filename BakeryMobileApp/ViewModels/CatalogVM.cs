using BakeryMobileApp.Helper;
using BakeryMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Session6.ViewModels
{
    public class CatalogVM
    {
        ApiHelper api = new ApiHelper();
        public ObservableCollection<Product> NonFavoriteProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> SavedNonFavoriteProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; set; } = new ();
        private readonly int _userId;
        public CatalogVM(int userId)
        {
            _userId = userId;
        }

        public async Task LoadInformation() 
        {
            await LoadCategories();

            await LoadProducts();

            LoadNonFavorites();
        }

        public async Task LoadCategories() 
        {
            Categories.Clear();

            var categories = await api.GetCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }


        public async Task LoadProducts() 
        {
            NonFavoriteProducts.Clear();
            SavedNonFavoriteProducts.Clear();

            var allProducts = await api.GetProducts();

            foreach (var p in allProducts)
            {
                NonFavoriteProducts.Add(p);
                SavedNonFavoriteProducts.Add(p);
            }
        }

        private void LoadNonFavorites()
        {
            var userPreferences = Preferences.Get($"@{_userId}_favorites", string.Empty);
            if (!string.IsNullOrEmpty(userPreferences))
            {
                var favoriteList = JsonSerializer.Deserialize<List<Product>>(userPreferences);
                foreach (var favorite in favoriteList)
                {
                    var itemToRemove = SavedNonFavoriteProducts.Where(p => p.Id == favorite.Id).FirstOrDefault();
                    if (itemToRemove != null) 
                    {
                        NonFavoriteProducts.Remove(itemToRemove);
                        SavedNonFavoriteProducts.Remove(itemToRemove);
                    }
                }
            }
        }
        public void Filter(List<Category> categories, string search) 
        {
            NonFavoriteProducts.Clear();

            var filteredNonFavorites = SavedNonFavoriteProducts.ToList();
            
            if (!string.IsNullOrEmpty(search)) 
                filteredNonFavorites = filteredNonFavorites.Where(x=> x.Name.ToLower().StartsWith(search.ToLower())).ToList();    


            if (categories.Count > 0)
            {
                var ids = categories.Select(c => c.Id).ToList();
                filteredNonFavorites = filteredNonFavorites.Where(x => ids.Contains((int)x.CategoryId)).ToList();
            }

            foreach (var nonFavorite in filteredNonFavorites)
                NonFavoriteProducts.Add(nonFavorite);
        }

        public void AddProductToFavorites(Product productToFavorite) 
        {
            var favoriteList = new List<Product>();
            
            var userPreferences = Preferences.Get($"@{_userId}_favorites", string.Empty);
            if (!string.IsNullOrEmpty(userPreferences))
                favoriteList = JsonSerializer.Deserialize<List<Product>>(userPreferences);
            
            favoriteList.Add(productToFavorite);
            var serializedProductList = JsonSerializer.Serialize(favoriteList);
            Preferences.Set($"@{_userId}_favorites", serializedProductList);
           
            SavedNonFavoriteProducts.Remove(productToFavorite);
            NonFavoriteProducts.Remove(productToFavorite);
        }
    }
}
