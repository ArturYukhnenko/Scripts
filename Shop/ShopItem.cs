using System;
using Exceptions;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

        public void SetItems(Sprite icon, String price, String available,String goodName) {
            iconHolder.sprite = icon;
            priceHolder.text = price;
            availableItems.text = available;
            buyBtn.onClick.AddListener(() => Buy(goodName));
        }

        private void Buy(string goodName) {
            try {
                StorageController.Instance.BuyItem(goodName,int.Parse(priceHolder.text));
                DecreaseAvailableItemsValue();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        public void SetAvailableItems(int available) {
            if (available < 0) {
                throw new WrongValueException("You are trying to set wrong available value, value: " + available);
            }

            availableItems.text = available.ToString();
        }

        private void DecreaseAvailableItemsValue() {
            int tmp = int.Parse(availableItems.text);
            if (tmp <= 0) {
                buyBtn.gameObject.SetActive(false);
                throw new Exception("There is not available items left");
            }

            if (tmp - 1 == 0) {
                buyBtn.gameObject.SetActive(false);
            }
            availableItems.text = (tmp-1).ToString();
        }

        public void SetNewPrice(int price) {
            if (price < 0) {
                throw new WrongValueException("You are trying to set wrong price value, value: " + price);
            }

            priceHolder.text = price.ToString();
        }
    }
}