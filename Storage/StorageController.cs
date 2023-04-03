using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MenuEquipment.SO;
using Models;
using Storage.SO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Storage {
    public class StorageController : MonoBehaviour {
        //SaveFiles
        private readonly string _dirPath = "Assets/Resourses/SavedData/Storage";
        private readonly string _fileName = "StorageData";
        
        //Cells
        private readonly List<StorageCell> _cellsInStorage = new List<StorageCell>();
        [SerializeField] 
        private GameObject cellPrefab;
        [SerializeField] 
        private GameObject cellsSpawner;
        [SerializeField]
        private Sprite cellDefaultSprite;
        [SerializeField]
        private int amountOfCells;

        //StorageItems
        [SerializeField] 
        private StorageHolder storageHolder;
        private Storage _currentStorage;
        [SerializeField]
        private RawComponents rawComponents;
        [SerializeField] 
        private Menu dishes;

        //Singleton
        private static StorageController _instance;

        public static StorageController Instance => _instance;

        private void Awake() {
            if (_instance != null && _instance != this) {
                Debug.Log("Singleton pattern is in dangerous. Destroying intruders in 3 2 1");
                Destroy(this.gameObject);
            } else {
                _instance = this;
                DontDestroyOnLoad(_instance);
            }
        }

        void Start() {
            if (_currentStorage == null) {
                if (storageHolder.Storage.StoredItems == null || storageHolder.Storage == null) {
                    Load(SaveAndLoad.SaveAndLoad.Load(_dirPath, _fileName));
                    _currentStorage = storageHolder.Storage;
                }else {
                    _currentStorage = storageHolder.Storage;
                }
            }
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
            SetItemsInCells();
        }

        //For tests
        void OnApplicationQuit() {
            Save();
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            StorageCell.DefaultImg = cellDefaultSprite;
            for (int i = 0; i < amountOfCells; i++) {
               GameObject cell = Instantiate(cellPrefab, cellsSpawner.transform);

               cell.name = i.ToString();

               StorageCell storageCell = new StorageCell {
                   ID = i,
                   ItemGameObject = cell
               };

               storageCell.ResetCell();

               RectTransform rt = cell.GetComponent<RectTransform>();
               rt.localPosition = new Vector3(0, 0, 0);
               rt.localScale = new Vector3(1, 1, 1);
               cell.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);

               _cellsInStorage.Add(storageCell);
            }
        }

        public void SetItemsInCells() {
            int i = 0;
            foreach (var ingredient in _currentStorage.StoredItems) {
                _cellsInStorage[i].ItemGameObject.GetComponent<Image>().sprite = ingredient.Key.Icon;
                _cellsInStorage[i].CellItemName = ingredient.Key.Name;
                _cellsInStorage[i].ItemGameObject.GetComponentInChildren<TMP_Text>().text = ingredient.Value.ToString();
                i++;
            }
        }
        
        public void ClearCellFromItems(string itemName) {
                    StorageCell cell = _cellsInStorage.First(i => i.CellItemName == itemName);
                    cell.ResetCell();
        }
        
        //StorageControl
        public void BuyItem(String itemName, int amountOfItems, int price) {
            try {
                _currentStorage.Coins = -price;
                RawComponents.RawIngredient ingredient = rawComponents.GetIngredient(itemName);
                _currentStorage.AddItem(ingredient,amountOfItems);
            }catch (Exception e) {
                Debug.Log(e);
            }
        }
        
        //Transferring SO to other different classes
        public Menu ReceiveActualDishes() {
            return dishes;
        }
        public RawComponents ReceiveActualComponents() {
            return rawComponents;
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

        //Method to use items from storage
        /// <summary>
        /// Method is used to get only one dish
        /// </summary>
        /// <param name="dishName"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public void GetDishFromStorage(string dishName) {
            if (dishes.IsDishExists(dishName)) {
                _currentStorage.UseItem(dishName, 1);
            }
            throw new Exception("Dish is not exists");
        }
        
        /// <summary>
        /// Method is used to get dish in bigger amount, than 1
        /// </summary>
        /// <param name="dishName"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public void GetDishFromStorage(string dishName, int amount) {
            if (dishes.IsDishExists(dishName)) {
                _currentStorage.UseItem(dishName, amount);
            }
            throw new Exception("Dish is not exists");
        }

        /// <summary>
        /// Method is used to get a few different dishes in any amount
        /// </summary>
        /// <param name="dishNames">List with Dish names, dish names can be duplicated in list</param>
        /// <exception cref="Exception">Throw exception if dish is not exists in list of all dishes</exception>
        public void GetDishFromStorage(List<string> dishNames) {
            Dictionary<string, int> usedDishes = new Dictionary<string, int>();
            foreach (string dish in dishNames) {
                if (!dishes.IsDishExists(dish)) {
                    throw new Exception("Dish is not exists");
                }
                if (usedDishes.ContainsKey(dish)) {
                    usedDishes[dish] += 1;
                }else {
                   usedDishes.Add(dish, 1); 
                }
                
            }
            _currentStorage.UseItem(usedDishes);
        }
        
        /// <summary>
        /// Method is used to get only one ingredient
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <exception cref="Exception"> Throw exception if ingredient is not exists in list of all ingredients</exception>
        public void GetIngredientFromStorage(string ingredientName) {
            if (rawComponents.IsIngredientExists(ingredientName)) {
                _currentStorage.UseItem(ingredientName, 1);
            }
            throw new Exception("Dish is not exists");
        }
        
        /// <summary>
        /// Method is used to get ingredient in bigger amount, than 1
        /// </summary>
        /// <param name="ingredientName"></param>
        /// <param name="amount"></param>
        /// <exception cref="Exception"> Throw exception if dish is not exists in list of all dishes</exception>
        public void GetIngredientFromStorage(string ingredientName, int amount) {
            if (rawComponents.IsIngredientExists(ingredientName)) {
                _currentStorage.UseItem(ingredientName, amount);
            }
            throw new Exception("Dish is not exists");
        }

        /// <summary>
        /// Method is used to get a few different ingredients in any amount
        /// </summary>
        /// <param name="ingredientNames">Dictionary with Ingredient name key(string) and amount of ingredients as value(int)</param>
        /// <exception cref="Exception">Throw exception if ingredient is not exists in list of all ingredients</exception>
        public void GetIngredientFromStorage(List<string> ingredientNames) {
            Dictionary<string, int> usedIngredients = new Dictionary<string, int>();
            foreach (string ingredient in ingredientNames) {
                if (!rawComponents.IsIngredientExists(ingredient)) {
                    throw new Exception("Ingredient is not exists");
                }
                if (usedIngredients.ContainsKey(ingredient)) {
                    usedIngredients[ingredient] += 1;
                }else {
                    usedIngredients.Add(ingredient, 1); 
                }
            }
            _currentStorage.UseItem(usedIngredients);
        }

        //Save and load Data
        private void Load(StorageModel data) {
            if (data != null) {
                Dictionary<IItem, int> ingredients =
                    new Dictionary<IItem, int>();
                foreach (string key in data.Keys) {
                    if (rawComponents.IsIngredientExists(key))
                        ingredients.Add(rawComponents.GetIngredient(key), data.Values[data.Keys.IndexOf(key)]);
                    if(dishes.IsDishExists(key))
                        ingredients.Add(dishes.GetDish(key), data.Values[data.Keys.IndexOf(key)]);
                }
                storageHolder.AssignStorage(data.maxCapacity,data.coins,ingredients);
            }else {
                storageHolder.CreateStorage();   
            }
        }

        private void Save() {
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

            SaveAndLoad.SaveAndLoad.Save(data,_dirPath,_fileName);
        }
        
    
    }
}