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
    public class FavoritesVM 
    {
        ApiHelper api = new ApiHelper();
        public ObservableCollection<Product> FavoriteProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> SavedFavoriteProducts { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; set; } = new();
        private readonly int _userId;
        public FavoritesVM(int userId)
        {
            _userId = userId;
        }

        public async Task LoadInformation()
        {
            await LoadCategories();

            LoadFavorites();
        }

        public async Task LoadCategories()
        {
            Categories.Clear();

            var categories = await api.GetCategories();
            foreach (var category in categories)
                Categories.Add(category);
        }


        private void LoadFavorites()
        {
            FavoriteProducts.Clear();
            SavedFavoriteProducts.Clear();

            var userPreferences = Preferences.Get($"@{_userId}_favorites", string.Empty);
            if (!string.IsNullOrEmpty(userPreferences))
            {
                var favoriteList = JsonSerializer.Deserialize<List<Product>>(userPreferences);
                foreach (var favorite in favoriteList)
                {
                    FavoriteProducts.Add(favorite);
                    SavedFavoriteProducts.Add(favorite);
                }
            }
        }
        public void Filter(List<Category> categories, string search)
        {
            FavoriteProducts.Clear();

            var filteredNonFavorites = SavedFavoriteProducts.ToList();

            if (!string.IsNullOrEmpty(search))
                filteredNonFavorites = filteredNonFavorites.Where(x => x.Name.ToLower().StartsWith(search.ToLower())).ToList();


            if (categories.Count > 0)
            {
                var ids = categories.Select(c => c.Id).ToList();
                filteredNonFavorites = filteredNonFavorites.Where(x => ids.Contains((int)x.CategoryId)).ToList();
            }

            foreach (var nonFavorite in filteredNonFavorites)
                FavoriteProducts.Add(nonFavorite);
        }

        public void RemoveProductFromFavorites(Product productToUnfavorite)
        {
            var favoriteList = new List<Product>();
            var userPreferences = Preferences.Get($"@{_userId}_favorites", string.Empty);
            if (!string.IsNullOrEmpty(userPreferences)) 
            {
                favoriteList = JsonSerializer.Deserialize<List<Product>>(userPreferences);
                var productToRemove = favoriteList.FirstOrDefault(x=> x.Id == productToUnfavorite.Id);
                favoriteList.Remove(productToRemove);
                var serializedProductList = JsonSerializer.Serialize(favoriteList);
                Preferences.Set($"@{_userId}_favorites", serializedProductList);

                FavoriteProducts.Remove(productToUnfavorite);
                SavedFavoriteProducts.Remove(productToUnfavorite);
            }

        }
    }
}
