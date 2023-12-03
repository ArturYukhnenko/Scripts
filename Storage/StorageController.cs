using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Exceptions;
using MenuEquipment.SO;
using Models;
using Storage.SO;
using UnityEngine;
using UnityEngine.Events;

namespace Storage {
    public class StorageController {
        //SaveFiles
        private const string DirPath = "/Storage";
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

        public event Action OnAddedItems, OnRemovedItems;

        private StorageController() { }

        public void SetFields(StorageHolder storageHolder, RawComponents rawComponents, Menu dishes) {
            _storageHolder = storageHolder;
            _rawComponents = rawComponents;
            _dishes = dishes;
            if (MainMenuController.NewGame) {
                _currentStorage = new Storage(1000, 1000);
            }else {
                if (_currentStorage == null) {
                    if (_storageHolder.Storage.StoredItems == null || _storageHolder.Storage == null) {
                        Load((StorageModel)SaveAndLoad.SaveAndLoad.Load(DirPath, FileName,
                            ModelTypesEnums.StorageModel));
                        _currentStorage = _storageHolder.Storage;
                    }
                    else {
                        _currentStorage = _storageHolder.Storage;
                    }
                }
            }
            
            _isSet = true;
        }

        //Transferring SO to other different classes
        public List<Menu.Dish> ReceiveActualDishes() {
            List<Menu.Dish> actualDishes = new List<Menu.Dish>();
            foreach (Menu.Dish dish in _dishes.dishes) {
                if (dish.activated) {
                    if (!dish.isIngredient)
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
            if (income >= 0)
                _currentStorage.Coins = income;
            else
                throw new WrongValueException("Number cannot be less or equals to 0");
        }

        public void BuyItem(string itemName,int price) {
            try {
                if (_rawComponents.IsIngredientExists(itemName)) {
                    if (!(_currentStorage.Coins - price < 0)) {
                        _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), 1);
                        _currentStorage.Coins = -price;
                        OnAddedItems?.Invoke();
                    }else {
                        throw new NotEnoughMoneyException("You don't have enough money to perform this action");
                    }
                }else { 
                    throw new ElementNotFoundException("Item not found exception");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Data);
                throw;
            }
        }
        public void BuyFurniture(string itemName,int price) {
            try {
                
                    if (!(_currentStorage.Coins - price < 0)) {
                        _currentStorage.Coins = -price;
                    }else {
                        throw new NotEnoughMoneyException("You don't have enough money to perform this action");
                    }
                
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void BuyItem(string itemName,int amount,int price) {
            try {
                if(!(_currentStorage.Coins - price*amount < 0)){
                    if (_rawComponents.IsIngredientExists(itemName)) {
                        _currentStorage.AddItem(_rawComponents.GetIngredient(itemName), amount);
                        _currentStorage.Coins = -price;
                        OnAddedItems?.Invoke();
                    }else {
                        throw new ElementNotFoundException("Item not found exception");
                    }
                }else {
                    throw new NotEnoughMoneyException("You don't have enough money to perform this action");
                }
            }catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public int GetAmountOfMoney() {
            return _currentStorage.Coins;
        }

       public void AddDishToStorage(string itemName) {
           try {
                if (_dishes.IsDishExists(itemName)) { 
                    _currentStorage.AddItem(_dishes.GetDish(itemName), 1);
                    OnAddedItems?.Invoke();
                }else { 
                    throw new ElementNotFoundException("Item not found exception");
                }
           }
           catch (Exception e) {
               Console.WriteLine(e);
               throw;
           }
       }
       
       public void AddDishToStorage(string itemName, int amount) {
           try {
                if (_dishes.IsDishExists(itemName)) {
                    _currentStorage.AddItem(_dishes.GetDish(itemName), amount);
                    OnAddedItems?.Invoke();
                }else { 
                    throw new ElementNotFoundException("Item not found exception");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
       }
        
        public void GetDishFromStorage(string dishName) {
            if (!_dishes.IsDishExists(dishName)) { 
                throw new ElementNotFoundException($"{dishName} is not exists "); 
            }
            try {
                Menu.Dish dish = _dishes.GetDish(dishName);
                _currentStorage.GetItem(dish,1);
            }
            catch (NotEnoughItemsException e) {
                throw new NotEnoughItemsException($"There is not enough {dishName} in storage");
            }
            OnRemovedItems?.Invoke();
        }
        
        public void GetDishFromStorage(List<string> dishes) {
            foreach (var dish in dishes) {
                if (!_dishes.IsDishExists(dish)) { 
                    throw new ElementNotFoundException($"{dish} is not exists "); 
                }
            }
            foreach (var dishName in dishes) {
                try { 
                    Menu.Dish dish = _dishes.GetDish(dishName);
                    _currentStorage.GetItem(dish,1);
                }
                catch (NotEnoughItemsException e) {
                    throw new NotEnoughItemsException($"There is not enough {dishName} in storage");
                }
            }
            OnRemovedItems?.Invoke();
        }

        public void GetIngredientFromStorage(string ingredient) {
           if (!_rawComponents.IsIngredientExists(ingredient)) { 
               throw new ElementNotFoundException($"{ingredient} is not exists "); 
           }
           try {
               RawComponents.RawIngredient ing = _rawComponents.GetIngredient(ingredient);
               _currentStorage.GetItem(ing, 1); 
           }catch (NotEnoughItemsException e) {
               Debug.LogError(e);
               throw new NotEnoughItemsException($"There is not enough {ingredient} in storage");
           }
           
           OnRemovedItems?.Invoke();
       }
        
        public void GetIngredientFromStorage(List<string> ingredients) {
            foreach (var ingredient in ingredients) {
                if (!_rawComponents.IsIngredientExists(ingredient)) { 
                    throw new ElementNotFoundException($"{ingredient} is not exists "); 
                }
            }
            foreach (var ingredient in ingredients) {
                try {
                    RawComponents.RawIngredient ing = _rawComponents.GetIngredient(ingredient);
                    _currentStorage.GetItem(ing, 1);
                }
                catch (NotEnoughItemsException e) {
                    throw new NotEnoughItemsException($"There is not enough {ingredient} in storage");
                }
            }
            OnRemovedItems?.Invoke();
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
                    if (_rawComponents.IsIngredientExists(key)) {
                        if (!ingredients.ContainsKey(_rawComponents.GetIngredient(key))) 
                            ingredients.Add(_rawComponents.GetIngredient(key), data.Values[data.Keys.IndexOf(key)]);
                        else 
                            ingredients[_rawComponents.GetIngredient(key)] += 1;
                    }
                    else if (_dishes.IsDishExists(key)) {
                        if (!ingredients.ContainsKey(_dishes.GetDish(key))) 
                            ingredients.Add(_dishes.GetDish(key), data.Values[data.Keys.IndexOf(key)]);
                        else 
                            ingredients[_dishes.GetDish(key)] += 1;
                    }
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