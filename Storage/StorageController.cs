using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Models;
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
        private GameObject cellPreset;
        [SerializeField] 
        private GameObject placeForCells;
        [SerializeField]
        private Sprite cellDefaultSprite;
        [SerializeField]
        private int amountOfCells;

        //StorageItems
        [SerializeField] 
        private StorageHolder storageData;
        private Storage _currentStorage;
        [SerializeField]
        private RawComponents componentsList;

        //Singleton
        private static StorageController _instance;

        public static StorageController Instance => _instance;

        private void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this.gameObject);
            } else {
                _instance = this;
                DontDestroyOnLoad(_instance);
            }
        }

        void Start() {
            if (_currentStorage == null) {
                if (storageData.Storage.StoredItems == null || storageData.Storage == null) {
                    Load(SaveAndLoad.SaveAndLoad.Load(_dirPath, _fileName) as StorageModel);
                    _currentStorage = storageData.Storage;
                }else {
                    _currentStorage = storageData.Storage;
                }
            }
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
            SetItemsInCells();
        }

        void OnApplicationQuit() {
            Save();
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            for (int i = 0; i < amountOfCells; i++) {
               GameObject cell = Instantiate(cellPreset, placeForCells.transform) as GameObject;

               cell.name = i.ToString();

               StorageCell storageCell = new StorageCell {
                   ID = i,
                   DefaultImg = cellDefaultSprite,
                   ItemGameObject = cell
               };

               storageCell.ItemGameObject.GetComponent<Image>().sprite = cellDefaultSprite;
               storageCell.ItemGameObject.GetComponentInChildren<TMP_Text>().text = "";

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
                _cellsInStorage[i].CellName = ingredient.Key.Name;
                _cellsInStorage[i].ItemGameObject.GetComponentInChildren<TMP_Text>().text = ingredient.Value.ToString();
                i++;
            }
        }
        
        public void ClearCellFromItems(string itemName) {
                    StorageCell cell = _cellsInStorage.First(i => i.CellName == itemName);
                    cell.ItemGameObject.GetComponent<Image>().sprite = cell.DefaultImg;
                    cell.ItemGameObject.GetComponentInChildren<TMP_Text>().text = "";
        }
        
        //StorageControl
        public void BuyItem(String itemName, int amount, int price) {
            try {
                _currentStorage.Coins = -price;
                RawComponents.RawIngredient ingredient = componentsList.GetIngredient(itemName);
                _currentStorage.AddItems(ingredient,amount);
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
            Debug.Log(data);
            if (data != null) {
                Dictionary<IItem, int> ingredients =
                    new Dictionary<IItem, int>();
                foreach (string key in data.Keys) {
                    ingredients.Add(componentsList.GetIngredient(key), data.Values[data.Keys.IndexOf(key)]);
                }
                storageData.AssignStorage(data.maxCapacity,data.coins,ingredients);
            }else {
                storageData.CreateStorage();   
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