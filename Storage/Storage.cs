using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Storage {
    [Serializable]
    public class Storage {
        
        private Dictionary<IItem, int> _storedItems;

        public ReadOnlyDictionary<IItem, int> StoredItems => _storedItems != null ? new ReadOnlyDictionary<IItem, int>(_storedItems) : null;

        private int _maxCapacity;
        private int _currentFilled;
        private int _coins;

        //Getters|Setters area
        public int MaxCapacity {
            get => _maxCapacity;
            set => _maxCapacity += value;
        }
    
        public int CurrentFilled => _currentFilled;

        public int Coins {
            get => _coins;
            set => _coins = _coins + value >= 0 ? + value : throw new Exception("Not enough money");
        }

        public Storage(int maxCapacity, int coins) { 
            _maxCapacity = maxCapacity; 
            _coins = coins;
            _storedItems = new Dictionary<IItem, int>();
             _currentFilled = 0;
        }
        
        public Storage(int maxCapacity, int coins, Dictionary<IItem, int> storedItems) { 
            _maxCapacity = maxCapacity; 
            _coins = coins; 
            _storedItems = storedItems; 
            foreach (var item in _storedItems) { 
                _currentFilled += storedItems[item.Key];
            }
        }

        public void AddItems(IItem ingredient, int amount) {
            if (ingredient == null) {
                throw new Exception("You are trying to add nullable value of item");
            }
            if (amount <= 0) {
                throw new Exception("Wrong amount of ingredients");
            }

            if (_storedItems.ContainsKey(ingredient)) {
                if (_currentFilled + amount > _maxCapacity) { 
                    throw new Exception("Not enough space in storage");
                }
                _storedItems[ingredient] += amount;
            }else if (!_storedItems.ContainsKey(ingredient)) {
                if (_currentFilled + amount > _maxCapacity) { 
                    throw new Exception("Not enough space in storage");
                }
                _storedItems.Add(ingredient, amount);
            }

            _currentFilled += amount;
            
            StorageController.Instance.SetItemsInCells();
        }

        ///<summary>
        /// This method is used to get a single item in any amount from storage
        ///</summary>
        public void UseItem(String itemName, int amount) {
            IItem ingredient = _storedItems.Keys.First(i => i.Name == itemName);
            if (_storedItems[ingredient] < amount) {
                throw new Exception($"Not enough{ingredient.Name} in storage");
            }else {
                if (_storedItems[ingredient] - amount == 0) {
                    RemoveItems(ingredient);
                }else {
                    _storedItems[ingredient] -= amount;
                    _currentFilled -= amount;
                }
            }
        }
        
        ///<summary>
        /// This method is used to get a multiple items from storage
        ///</summary>
        public void UseItem(Dictionary<string,int> items) {
            foreach (var itemName in items) {
                IItem ingredient = _storedItems.Keys.First(i => i.Name == itemName.Key);
                            if (_storedItems[ingredient] < itemName.Value) {
                                throw new Exception($"Not enough{ingredient.Name} in storage");
                            }else {
                                if (_storedItems[ingredient] - itemName.Value == 0) {
                                    RemoveItems(ingredient);
                                }else {
                                    _storedItems[ingredient] -= itemName.Value;
                                    _currentFilled -= itemName.Value;
                                }
                            }
            }
        }

        public void RemoveItems(IItem ingredient) {
            if (ingredient == null) {
                throw new Exception("You are trying to remove nullable value of item");
            }
            if (!_storedItems.ContainsKey(ingredient)) {
                throw new Exception("Item not found");
            }

            StorageController.Instance.ClearCellFromItems(ingredient.Name);
            
            if (_storedItems[ingredient] > 0) {
                _currentFilled -= _storedItems[ingredient];
                _storedItems.Remove(ingredient);
            }else {
                _storedItems.Remove(ingredient);
            }
        }

    }
}