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

        public Menu ReceiveActualDishes() {
            return dishes;
        }

        public RawComponents ReceiveActualComponents() {
            return rawComponents;
        }

        public void UseItemFromStorage(string itemName, int value) {
            if (itemName == null) {
                throw new Exception("Items not set");
            }
            try {
                _currentStorage.UseItem(itemName, value);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
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