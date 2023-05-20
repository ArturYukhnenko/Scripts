using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MenuEquipment.SO;
using Models;
using Storage.SO;

namespace Storage {
    public class StorageController {
        //SaveFiles
        private const string DirPath = "Assets/Resourses/SavedData/Storage";
        private const string FileName = "StorageData";

        //StorageItems 
        private StorageHolder _storageHolder;
        private static Storage _currentStorage;
        private RawComponents _rawComponents;
        private Menu _dishes;

        public ReadOnlyDictionary<IItem, int> StoredItems => _currentStorage.StoredItems;

        //Singleton
        private static readonly StorageController _instance = new StorageController();
        public static StorageController Instance => _instance;
        
        
        private bool _isSet = false;
        public bool IsSet => _isSet;
        
        private StorageController() { }

        public void SetFields(StorageHolder storageHolder, RawComponents rawComponents, Menu dishes) {
            _storageHolder = storageHolder;
            _rawComponents = rawComponents;
            _dishes = dishes;
            if (_currentStorage == null) {
                if (_storageHolder.Storage.StoredItems == null || _storageHolder.Storage == null) {
                    Load((StorageModel)SaveAndLoad.SaveAndLoad.Load(DirPath, FileName, ModelTypesEnums.StorageModel));
                    _currentStorage = _storageHolder.Storage;
                }else {
                    _currentStorage = _storageHolder.Storage;
                }
            }
            _isSet = true;
        }

        //Transferring SO to other different classes
        public List<Menu.Dish> ReceiveActualDishes() {
            List<Menu.Dish> actualDishes = new List<Menu.Dish>();
            foreach (Menu.Dish dish in _dishes.dishes) {
                if (dish.activated) {
                    actualDishes.Add(dish);
                }
            }

            if (actualDishes.Count == 0) {
                throw new Exception("Actual menu doesn't contain any dishes");
            }

            return actualDishes;
        }
        public RawComponents ReceiveActualComponents() {
            return _rawComponents;
        }
        
        //Money managment
        public void AddEarnedMoney(int income) {
            _currentStorage.Coins = income;
        }

        public void SpendMoney(int price) {
            if (price < 0) {
                _currentStorage.Coins = price;
            }else {
                _currentStorage.Coins = -price;
            }
        }

        public int GetMoney() {
            return _currentStorage.Coins;
        }

        /// <summary>
        /// Method is used to add item to the storage in single existance
        /// </summary>
        /// <param name="itemName"></param>
        /// <exception cref="Exception">Throw exception if item not found in any list of existing items</exception>
        public void AddItemToStorage(string itemName) {
            if (_dishes.IsDishExists(itemName)) {
                _currentStorage.AddItem(_dishes.GetDish(itemName), 1);
            }else if (_rawComponents.IsIngredientExists(itemName)) {
                _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), 1);
            }else{
                throw new Exception("Item not found exception");
            }
        } 
        
        /// <summary>
        /// Method is used to add item to the storage in any amount
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception">Throw exception if item not found in any list of existing items</exception>
        public void AddItemToStorage(string itemName, int amount) {
            if (_dishes.IsDishExists(itemName)) {
                _currentStorage.AddItem(_dishes.GetDish(itemName), amount);
            }else if (_rawComponents.IsIngredientExists(itemName)) {
                _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), amount);
            }else {
                throw new Exception("Item not found exception");
            }
        }

        //Method to use items from storage
        /// <summary>
        /// Method is used to get only one dish
        /// </summary>
        /// <param name="dishName"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public int GetDishFromStorage(string dishName) {
            int price = 0;
            if (!_dishes.IsDishExists(dishName)) {
                throw new Exception("Dish is not exists"); 
            }
            price = _currentStorage.UseItem(dishName, 1);
            return price;
        }
        
        /// <summary>
        /// Method is used to get dish in bigger amount, than 1
        /// </summary>
        /// <param name="dishName"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public int GetDishFromStorage(string dishName, int amount) {
            int price = 0;
            if (!_dishes.IsDishExists(dishName)) {
                throw new Exception("Dish is not exists");
            }
            price = _currentStorage.UseItem(dishName, amount);
            return price;
        }

        /// <summary>
        /// Method is used to get a few different dishes in any amount
        /// </summary>
        /// <param name="dishNames">List with Dish names, dish names can be duplicated in list</param>
        /// <exception cref="Exception">Throw exception if dish is not exists in list of all dishes</exception>
        public int GetDishFromStorage(List<string> dishNames) {
            int price = 0;
            Dictionary<string, int> usedDishes = new Dictionary<string, int>();
            foreach (string dish in dishNames) {
                if (!_dishes.IsDishExists(dish)) {
                    throw new Exception("Dish is not exists");
                }
                if (usedDishes.ContainsKey(dish)) {
                    usedDishes[dish] += 1;
                }else {
                   usedDishes.Add(dish, 1); 
                }
            }

            try {
                price = _currentStorage.UseItem(usedDishes);
            }catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            return price;
        }
        
        /// <summary>
        /// Method is used to get only one ingredient
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <exception cref="Exception"> Throw exception if ingredient is not exists in list of all ingredients</exception>
        public int GetIngredientFromStorage(string ingredientName) {
            int price = 0;
            if (!_rawComponents.IsIngredientExists(ingredientName)) {
                throw new Exception("Dish is not exists");
            }
            price = _currentStorage.UseItem(ingredientName, 1);
            return price;
        }
        
        /// <summary>
        /// Method is used to get ingredient in bigger amount, than 1
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public int GetIngredientFromStorage(string ingredientName, int amount) {
            int price = 0;
            if (!_rawComponents.IsIngredientExists(ingredientName)) {
                throw new Exception("Dish is not exists");
            }
            price = _currentStorage.UseItem(ingredientName, amount);
            return price;
        }

        /// <summary>
        /// Method is used to get a few different ingredients in any amount
        /// </summary>
        /// <param name="ingredientNames">Dictionary with Ingredient name key(string) and amount of ingredients as value(int)</param>
        /// <exception cref="Exception">Throw exception if ingredient is not exists in list of all ingredients</exception>
        public int GetIngredientFromStorage(List<string> ingredientNames) {
            int price = 0;
            Dictionary<string, int> usedIngredients = new Dictionary<string, int>();
            foreach (string ingredient in ingredientNames) {
                if (!_rawComponents.IsIngredientExists(ingredient)) {
                    throw new Exception("Ingredient is not exists");
                }
                if (usedIngredients.ContainsKey(ingredient)) {
                    usedIngredients[ingredient] += 1;
                }else {
                    usedIngredients.Add(ingredient, 1); 
                }
            }
            
            try {
                price = _currentStorage.UseItem(usedIngredients);
            }catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            return price;
        }

        //Save and load Data
        private void Load(StorageModel data) {
            if (data != null) {
                Dictionary<IItem, int> ingredients =
                    new Dictionary<IItem, int>();
                foreach (string key in data.Keys) {
                    if (_rawComponents.IsIngredientExists(key))
                        ingredients.Add(_rawComponents.GetIngredient(key), data.Values[data.Keys.IndexOf(key)]);
                    if(_dishes.IsDishExists(key))
                        ingredients.Add(_dishes.GetDish(key), data.Values[data.Keys.IndexOf(key)]);
                }
                _storageHolder.AssignStorage(data.maxCapacity,data.coins,ingredients);
            }else {
                _storageHolder.CreateStorage();   
            }
        }

        public void Save() {
            List<string> keys = new List<string>();
            List<int> values = new List<int>();
            if (_currentStorage.StoredItems.Keys != null)
                foreach (IItem item in _currentStorage.StoredItems.Keys) {
                    keys.Add(item.Name);
                    values.Add(_currentStorage.StoredItems[item]);
                }
            else
                throw new Exception("Storage is empty");

            StorageModel data = new StorageModel {
                Keys = keys,
                Values = values,
                maxCapacity = _currentStorage.MaxCapacity,
                coins = _currentStorage.Coins
            };

            SaveAndLoad.SaveAndLoad.Save(data,DirPath,FileName);
        }


    }
}