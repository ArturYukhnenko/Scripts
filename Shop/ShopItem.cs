using System;
using System.Collections.Generic;
using Exceptions;
using MenuEquipment.SO;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

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

        [SerializeField] private Menu dishList;
        [SerializeField]private GameObject dishImagePrefab, dishSpawner;

        private String _goodName;
        private List<Menu.Dish> dishesToCook = new List<Menu.Dish>();
        

        public void SetItem(Sprite icon, String price,String goodName) {
            iconHolder.sprite = icon;
            priceHolder.text = price;
            availableItems.text = "0";
            buyBtn.onClick.AddListener(() => Buy(goodName));
            _goodName = goodName;
            SetDishes();
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

        private List<Menu.Dish> SetDishes()
        {
            foreach (var dish in dishList.dishes)
            {
                foreach (var dishIngredient in dish.ingredients)
                {
                    if (_goodName == dishIngredient)
                    {
                        dishesToCook.Add(dish);
                    }
                }

            }

            Debug.Log(dishesToCook.Count);
            foreach (var dish in dishesToCook)
            {
                var newPrefab = Instantiate(dishImagePrefab, dishSpawner.transform);
                newPrefab.GetComponent<Image>().sprite = dish.Icon;
                newPrefab.name = dish.Name + "Image";
            }
            return dishesToCook;
        }


        public Image IconHolder => iconHolder;

        public TMP_Text PriceHolder => priceHolder;

        public TMP_Text AvailableItems => availableItems;

        public Button BuyBtn => buyBtn;

        public string GoodName => _goodName;
        
    }
}