using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Storage {
    public class StorageDataBase {
        public readonly Dictionary<RawComponents.RawIngredient, int> StoredItems;
    
        private int _maxCapacity;
        private int _currentFilled;
        private int _coins;
    
        public StorageDataBase(int maxCapacity, int coins) {
            _maxCapacity = maxCapacity;
            _coins = coins;
            StoredItems = new Dictionary<RawComponents.RawIngredient, int>();
            _currentFilled = 0;
        }
    
        //Getters|Setters area
        public int MaxCapacity {
            get => _maxCapacity;
            set => _maxCapacity += value;
        }
    
        public int CurrentFilled {
            get => _currentFilled / _maxCapacity * 100;
            private set => _currentFilled = value;
        }
    
        public int Coins {
            get => _coins;
            set => _coins += value;
        }

        public void AddItems(RawComponents.RawIngredient item, int amount) {
            if (item == null) {
                throw new Exception("You are trying to add nullable value of item");
            }
            if (amount <= 0) {
                throw new Exception("Wrong amount of ingredients");
            }

            if (StoredItems.ContainsKey(item)) {
                StoredItems[item] += amount;
            }else if (!StoredItems.ContainsKey(item)) {
                StoredItems.Add(item, amount);
            }

            _currentFilled += amount;

        }

        public void GetItem(String itemName, int amount) {
            RawComponents.RawIngredient item = StoredItems.Keys.First(i => i.ingredientName == itemName);
            if (StoredItems[item] < amount) {
                throw new Exception($"Not enough{item.ingredientName} in storage");
            }else {
                if (StoredItems[item] - amount == 0) {
                    RemoveItems(item);
                }else {
                    StoredItems[item] -= amount;
                    _currentFilled -= amount;
                }
            }
        }
        
        public void GetItems(Dictionary<string,int> items) {
            foreach (KeyValuePair<string,int> itemName in items) {
                RawComponents.RawIngredient item = StoredItems.Keys.First(i => i.ingredientName == itemName.Key);
                            if (StoredItems[item] < itemName.Value) {
                                throw new Exception($"Not enough{item.ingredientName} in storage");
                            }else {
                                if (StoredItems[item] - itemName.Value == 0) {
                                    RemoveItems(item);
                                }else {
                                    StoredItems[item] -= itemName.Value;
                                    _currentFilled -= itemName.Value;
                                }
                            }
            }
        }

        public void RemoveItems(RawComponents.RawIngredient item) {
            if (item == null) {
                throw new Exception("You are trying to remove nullable value of item");
            }
            if (!StoredItems.ContainsKey(item)) {
                throw new Exception("Item not found");
            }

            if (StoredItems[item] != 0) {
                _currentFilled -= StoredItems[item];
                StoredItems.Remove(item);
            }else {
                StoredItems.Remove(item);
            }
        }
        
    }
}