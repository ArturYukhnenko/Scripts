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
            if (MainMenuController.NewGame) {
                CreateNewItems();
            }else {
                ShopModel data = (ShopModel)SaveAndLoad.SaveAndLoad.Load(DirPath, FileName, ModelTypesEnums.ShopModel);
                if (data != null && data.shopItems.Count > 0) {
                    Load(data);
                }
                else {
                    CreateNewItems();
                }
            }
            
            
        }

        private void CreateNewItems() {
            foreach (var t in goods.rawComponents) {
                GameObject good = Instantiate(shopCell, spawnPoint.transform);
                good.GetComponent<ShopItem>().SetItem(t.Icon,t.Price.ToString(),"100",t.Name);
                _shopItems.Add(good);
            }
        }

        public void SetNewAmountAvailable(string item, int amount) {
            _shopItems.First(i => i.GetComponent<ShopItem>().GoodName.Equals(item)).GetComponent<ShopItem>().SetAvailableItems(amount);
        }

        private void OnDestroy() {
            Save();
        }
        
        private void Load(ShopModel data) {
            _shopItems = new List<GameObject>();
            foreach (ShopItemHolder item in data.shopItems) {
                GameObject good = Instantiate(shopCell, spawnPoint.transform);
                good.GetComponent<ShopItem>().SetItem(goods.GetIngredient(item.goodName).Icon,item.price,item.available,item.goodName);
                _shopItems.Add(good);
            }
        }
        
        private void Save() {
            List<ShopItemHolder> tmpList = new List<ShopItemHolder>();
            foreach (GameObject shopItem in _shopItems) {
                tmpList.Add(new ShopItemHolder {
                    price = shopItem.GetComponent<ShopItem>().PriceHolder.text,
                    available = shopItem.GetComponent<ShopItem>().AvailableItems.text,
                    goodName = shopItem.GetComponent<ShopItem>().GoodName
                });
            }
            ShopModel shopModel = new ShopModel {
                shopItems = tmpList
            };
            SaveAndLoad.SaveAndLoad.Save(shopModel,DirPath,FileName);
        }

        
    }
}
