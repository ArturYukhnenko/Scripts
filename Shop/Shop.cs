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
    public class Shop : MonoBehaviour {
        [SerializeField]
        private GameObject shopCell;
        [SerializeField]
        private GameObject spawnPoint;
        [SerializeField]
        private RawComponents goods;
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
            foreach (var t in goods.rawComponents) {
                GameObject good = Instantiate(shopCell, spawnPoint.transform);
                good.GetComponentInChildren<Image>().sprite = t.Icon;
                good.GetComponentInChildren<TMP_Text>().text = t.Price.ToString();
                good.GetComponentInChildren<TMP_Text>().text = availableItems.ToString();
                good.GetComponentInChildren<Button>().onClick.AddListener(() => Buy(t.Name,t.Price,good));
            }
        }

        private void Buy(string goodName,int price,GameObject good) {
            try {
                int amount = int.Parse(good.GetComponentInChildren<TMP_Text>().text)-1;
                if (amount == 0) 
                    good.GetComponentInChildren<Button>().GameObject().SetActive(false);
                else if(amount < 0)
                    throw new NotEnoughItemsException("Available items in store less than");
                
                StorageController.Instance.BuyItem(goodName,price);
                good.GetComponentInChildren<TMP_Text>().text = amount.ToString();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            
        }

    }
}
