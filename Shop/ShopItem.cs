using System;
using Exceptions;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop {
    public class ShopItem : MonoBehaviour{
        
        [SerializeField]
        private Image iconHolder;
        [SerializeField]
        private TMP_Text priceHolder;
        [SerializeField]
        private TMP_Text availableItems;
        [SerializeField]
        private Button buyBtn;

        private String _goodName;

        public void SetItem(Sprite icon, String price,String goodName) {
            iconHolder.sprite = icon;
            priceHolder.text = price;
            availableItems.text = "0";
            buyBtn.onClick.AddListener(() => Buy(goodName));
            _goodName = goodName;
        }

        private void Buy(string goodName) {
            try {
                StorageController.Instance.BuyItem(goodName,int.Parse(priceHolder.text));
                int amount = int.Parse(availableItems.text);
                amount++;
                availableItems.text = amount.ToString();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public void SetNewPrice(int price) {
            if (price < 0) {
                throw new WrongValueException("You are trying to set wrong price value, value: " + price);
            }

            priceHolder.text = price.ToString();
        }

        public Image IconHolder => iconHolder;

        public TMP_Text PriceHolder => priceHolder;

        public TMP_Text AvailableItems => availableItems;

        public Button BuyBtn => buyBtn;

        public string GoodName => _goodName;
        
    }
}