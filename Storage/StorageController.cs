using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Storage {
    public class StorageController : MonoBehaviour {
        //Cells
        private List<StorageCell> _cellsInStorage = new List<StorageCell>();
        [SerializeField] 
        private GameObject cellPreset;
        [SerializeField] 
        private GameObject placeForCells;
        [SerializeField]
        private int amountOfCells;

        //StorageItems
        private StorageDataBase _currentStorage;
        [SerializeField]
        private int maxCapacity;
        [SerializeField] 
        private int startIncome;
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
            }
        }

        public void Start() {
            _currentStorage ??= new StorageDataBase(maxCapacity, startIncome);
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            for (int i = 0; i < amountOfCells; i++) {
               GameObject cell = Instantiate(cellPreset, placeForCells.transform) as GameObject;

               cell.name = i.ToString();

               StorageCell storageCell = new StorageCell {
                   ID = i,
                   ItemGameObject = cell
               };

               RectTransform rt = cell.GetComponent<RectTransform>();
               rt.localPosition = new Vector3(0, 0, 0);
               rt.localScale = new Vector3(1, 1, 1);
               cell.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);

               _cellsInStorage.Add(storageCell);
            }
        }

        public void SetItemsInCells() {
            int i = 0;
            foreach (KeyValuePair<RawComponents.RawIngredient, int> ingredient in _currentStorage.StoredItems) {
                _cellsInStorage[i].ItemGameObject.GetComponent<Image>().sprite = ingredient.Key.icon;
                _cellsInStorage[i].ItemName = ingredient.Key.ingredientName;
                _cellsInStorage[i].ItemGameObject.GetComponentInChildren<TMP_Text>().text = ingredient.Value.ToString();
                i++;
            }
        }
        
        public void ClearCellFromItems(string itemName) {
                    StorageCell cell = _cellsInStorage.First(i => i.ItemName == itemName);
                    cell.ItemGameObject.GetComponent<Image>().sprite = cell.DefaultImg;
        }
        
        //StorageControl
        public void BuyItem(String itemName, int amount, int price) {
            RawComponents.RawIngredient ingredient = componentsList.GetIngredient(itemName);
            _currentStorage.AddItems(ingredient,amount);
            _currentStorage.Coins = -price;
            SetItemsInCells();
        }

        public void TestAddItem(String itemName) {
            RawComponents.RawIngredient ingredient = componentsList.GetIngredient(itemName);
            _currentStorage.AddItems(ingredient,20);
            SetItemsInCells();
        }
        
        public void Sell(RawComponents.RawIngredient ingredient) {
            _currentStorage.RemoveItems(ingredient);
            SetItemsInCells();
        }
    
    }

}