using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Exceptions;
using MenuEquipment.SO;
using Models;
using Storage.SO;
using UnityEngine;

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
        
        //Money managment
        public void AddEarnedMoney(int income) {
            if (income > 0)
                _currentStorage.Coins = income;
            else
                throw new WrongValueException("Number cannot be less or equals to 0");
        }

        public void SpendMoney(int price) {
            if (price > 0)
                _currentStorage.Coins = -price;
            else
                throw new WrongValueException("Number cannot be less or equals to 0");
        }

        public int GetAmountOfMoney() {
            return _currentStorage.Coins;
        }

       public void AddItemToStorage(string itemName) {
            if (_dishes.IsDishExists(itemName)) {
                _currentStorage.AddItem(_dishes.GetDish(itemName), 1);
            }else if (_rawComponents.IsIngredientExists(itemName)) {
                _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), 1);
            }else {
                throw new ElementNotFoundException("Item not found exception");
            }
       }
       public void AddItemToStorage(string itemName, int amount) {
            if (_dishes.IsDishExists(itemName)) {
                _currentStorage.AddItem(_dishes.GetDish(itemName), amount);
            }else if (_rawComponents.IsIngredientExists(itemName)) {
                _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), amount);
            }else {
                throw new ElementNotFoundException("Item not found exception");
            }
       }
        
        public void GetDishFromStorage(string dish) {
            if (!_dishes.IsDishExists(dish)) { 
                throw new ElementNotFoundException($"{dish} is not exists "); 
            }
            if (!IfItemInStorage(dish)) { 
                throw new NotEnoughItemsException($"There is not enough {dish} in storage");
            }
            
            _currentStorage.GetItem(dish,1);
        }
        
       public void GetIngredientFromStorage(string ingredient) {
           if (!_dishes.IsDishExists(ingredient)) { 
               throw new ElementNotFoundException($"{ingredient} is not exists "); 
           }
           if (!IfItemInStorage(ingredient)) { 
               throw new NotEnoughItemsException($"There is not enough {ingredient} in storage");
           }

           _currentStorage.GetItem(ingredient, 1);
       }

        public bool IfItemInStorage(string item) {
            return _currentStorage.CheckItemForExistence(item);
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