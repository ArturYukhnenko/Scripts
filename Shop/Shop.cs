using System;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using Models;
using Storage;
using Storage.SO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shop {
    public class Shop : MonoBehaviour {
        
        private const string DirPath = "/Shop";
        private const string FileName = "ShopData";

        [SerializeField]
        private GameObject shopCell;
        [SerializeField]
        private GameObject spawnPoint;
        [SerializeField]
        private RawComponents goods;
        private List<GameObject> _shopItems = new List<GameObject>();

        public static List<string> LastBuy { get; set; } = new List<string>();

        public bool IsSomethingBought { get; set; }

        public void Awake() {
            foreach (var t in goods.rawComponents) {
                GameObject good = Instantiate(shopCell, spawnPoint.transform);
                good.GetComponent<ShopItem>().SetItem(t.Icon,t.Price.ToString(),t.Name);
                _shopItems.Add(good);
            }
        }

    }
}
