using System;
using System.Collections.Generic;
using Exceptions;
using Storage;
using Storage.SO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop {
    public class FurnitureShop : MonoBehaviour {
        [SerializeField]
        private GameObject shopCell;
        [SerializeField]
        private GameObject spawnPoint, buildSystem;
        [SerializeField]
        private FurnitureSO furniture;
        [SerializeField]
        private int availableItems;

        private bool _isSomethingBought;

        public int AvailableItems {
            get => availableItems;
            set => availableItems = value;
        }

        public void Awake() { 
            SpawnCells();
        }

        private void SpawnCells() {
            foreach (var t in furniture.rawComponents) {
                GameObject good = Instantiate(shopCell, spawnPoint.transform);
                good.GetComponentInChildren<Image>().sprite = t.Icon;
                good.GetComponentInChildren<TMP_Text>().text = t.Price.ToString();
                //good.GetComponentInChildren<TMP_Text>().text = availableItems.ToString();
                good.GetComponentInChildren<Button>().onClick.AddListener(() => Buy(t.Name,t.Price,good));
            }
        }

        private void Buy(string goodName,int price,GameObject good) {
            try {
                //int amount = int.Parse(good.GetComponentInChildren<TMP_Text>().text)-1;
                //if (amount == 0) 
                    //good.GetComponentInChildren<Button>().GameObject().SetActive(false);
                //else if(amount < 0)
                    //throw new NotEnoughItemsException("Available items in store less than");
                
                //
                //good.GetComponentInChildren<TMP_Text>().text = (int.Parse(good.GetComponentInChildren<TMP_Text>().text)-1).ToString();
                GameObject ourBuildSystem = Instantiate(buildSystem);
                ourBuildSystem.gameObject.GetComponent<PlacementSystem>().StartPlacement(goodName);
                //StorageController.Instance.BuyItem(goodName,price);
                Destroy(this.gameObject);
                
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            
        }

    }
}
