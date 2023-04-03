using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private int amountOfCells;

        //StorageItems
        [SerializeField] 
        private StorageHolder storageHolder;
        private Storage _currentStorage;
        [SerializeField]
        private RawComponents rawComponents;

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
            for (int i = 0; i < amountOfCells; i++) {
               GameObject cell = Instantiate(cellPrefab, cellsSpawner.transform);

               cell.name = i.ToString();

               StorageCell storageCell = cell.AddComponent<StorageCell>();

               storageCell.ID = i;
               storageCell.ItemGameObject = cell;
               
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

        public void SpendMoney(int price) {
            try {
                _currentStorage.Coins = price;
            }catch (Exception e) {
                Debug.Log(e);
            }
        }
        
        public void GetItemBasicPrice(string itemName) {
            Debug.Log(_currentStorage.StoredItems.First(i => i.Key.Name == itemName));
        }

        //Save and load Data
        private void Load(StorageModel data) {
            if (data != null) {
                Dictionary<IItem, int> ingredients =
                    new Dictionary<IItem, int>();
                foreach (string key in data.Keys) {
                    ingredients.Add(rawComponents.GetIngredient(key), data.Values[data.Keys.IndexOf(key)]);
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
                    var key = (RawComponents.RawIngredient)item;
                    keys.Add(key.Name);
                    values.Add(_currentStorage.StoredItems[key]);
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