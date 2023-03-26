using System;
using System.Collections.Generic;
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
            if (_currentStorage == null) {
                _currentStorage = new StorageDataBase(maxCapacity, startIncome);
                Debug.Log(_currentStorage.Coins);
            }
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            for (int i = 0; i < 27; i++) {
               GameObject cell = Instantiate(cellPreset, placeForCells.transform) as GameObject;

               cell.name = i.ToString();

               StorageCell storageCell = new StorageCell {
                   itemGameObject = cell
               };

               RectTransform rt = cell.GetComponent<RectTransform>();
               rt.localPosition = new Vector3(0, 0, 0);
               rt.localScale = new Vector3(1, 1, 1);
               cell.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);

               Button tempButton = cell.GetComponent<Button>();
               
               _cellsInStorage.Add(storageCell);
            }
        }

        public void TestBuy() {
            BuyItem("Milk",20,20);
            SetItemsInCells();
        }
        
        public void SetItemsInCells() {
            int i = 0;
            foreach (KeyValuePair<RawComponents.RawIngredient, int> ingredient in _currentStorage.StoredItems) {
                _cellsInStorage[i].itemGameObject.GetComponent<Image>().sprite = ingredient.Key.icon;
                _cellsInStorage[i].itemGameObject.GetComponentInChildren<TMP_Text>().text = ingredient.Value.ToString();
                i++;
            }
        }
        
        //StorageControl
        public void BuyItem(String itemName, int amount, int price) {
            RawComponents.RawIngredient ingredient = componentsList.GetIngredient(itemName);
            Debug.Log(_currentStorage);
            _currentStorage.AddItems(ingredient,amount);
            _currentStorage.Coins = -price;
        }
        
        public void Sell(RawComponents.RawIngredient ingredient) {
            _currentStorage.RemoveItems(ingredient);
        }
    
    }

    [Serializable]
    public class StorageCell {
        public int id;
        public GameObject itemGameObject;
    }
    
}