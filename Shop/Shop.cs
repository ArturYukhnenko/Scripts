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
                good.GetComponent<ShopItem>().SetItems(t.Icon,t.Price.ToString(),availableItems.ToString(),t.Name);
            }
        }

    }
}
