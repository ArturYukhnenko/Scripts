using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MenuEquipment.SO;
using Storage.SO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Storage {
    public class StorageManager : MonoBehaviour {
        
        //Cells
        private readonly List<StorageCell> _cellsInStorage = new List<StorageCell>();
        [SerializeField] 
        private GameObject cellPrefab;
        [SerializeField] 
        private GameObject cellsSpawner;
        [SerializeField] 
        private Sprite defaultStorageItemImage;
        [SerializeField]
        private int amountOfCells;
        
        //Stored Items
        [SerializeField]
        private StorageHolder storageHolder;
        [SerializeField]
        private RawComponents rawComponents;
        [SerializeField]
        private Menu dishes;

        public void Awake() {
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
            if (StorageController.Instance != null) {
                if(!StorageController.Instance.IsSet){
                    StorageController.Instance.SetFields(this,storageHolder,rawComponents,dishes);
                }
                SetItemsInCells();
            }
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            for (int i = 0; i < amountOfCells; i++) {
                GameObject cell = Instantiate(cellPrefab, cellsSpawner.transform);

                cell.name = i.ToString();

                StorageCell storageCell = new StorageCell() {
                    ID = i,
                    DefaultImg = defaultStorageItemImage,
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
            foreach (var ingredient in StorageController.Instance.StoredItems) {
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
    }
}