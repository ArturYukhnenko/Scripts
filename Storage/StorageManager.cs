using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

        public void Awake() {
            if (_cellsInStorage.Count == 0) {
                SpawnCells();
            }
            SetItemsInCells();
        }

        //Spawn and display cells in storage
        private void SpawnCells() {
            for (int i = 0; i < StorageController.Instance.StoredItems.Keys.Count; i++) {
                GameObject cell = Instantiate(cellPrefab, cellsSpawner.transform);

                cell.name = i.ToString();

                StorageCell storageCell = new StorageCell() {
                    ID = i,
                    DefaultImg = defaultStorageItemImage,
                    ItemGameObject = cell
                };

                storageCell.ResetCell();
                
                _cellsInStorage.Add(storageCell);
            }
        }

        private void SetItemsInCells() {
            int i = 0;
            foreach (var cell in _cellsInStorage.Where(cell => cell.CellItemName != "")) {
                cell.ResetCell();
            }
            foreach (var ingredient in StorageController.Instance.StoredItems) {
                _cellsInStorage[i].ItemGameObject.GetComponent<Image>().sprite = ingredient.Key.Icon;
                _cellsInStorage[i].CellItemName = ingredient.Key.Name;
                _cellsInStorage[i].ItemGameObject.GetComponentInChildren<TMP_Text>().text = ingredient.Value.ToString();
                i++;
            }
        }

        public void Save() {
            StorageController.Instance.Save();
        }
    }
}