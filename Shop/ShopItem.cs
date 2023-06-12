using System;
using Exceptions;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop {
    public class ShopItem : MonoBehaviour, IPointerClickHandler{
        
        [SerializeField]
        private Image iconHolder;
        [SerializeField]
        private TMP_Text priceHolder;
        [SerializeField]
        private TMP_Text availableItems;
        [SerializeField]
        private Button buyBtn;

        private String _goodName;

        public void SetItem(Sprite icon, String price, String available,String goodName) {
            iconHolder.sprite = icon;
            priceHolder.text = price;
            availableItems.text = available;
            buyBtn.onClick.AddListener(() => Buy(goodName));
            _goodName = goodName;
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
        
        private void Buy(string goodName, int amount) {
            try {
                StorageController.Instance.BuyItem(goodName,amount,int.Parse(priceHolder.text));
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

        public Image IconHolder => iconHolder;

        public TMP_Text PriceHolder => priceHolder;

        public TMP_Text AvailableItems => availableItems;

        public Button BuyBtn => buyBtn;

        public string GoodName => _goodName;
        
        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.pointerId == -1) {
                Buy(_goodName);
            }
            if (eventData.pointerId == -2) {
                
            }
        }
    }
}