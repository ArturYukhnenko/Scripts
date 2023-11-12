using System;
using System.Collections.Generic;
using MenuEquipment.SO;
using UnityEngine;

namespace Ordering {
    public class Order {

        private static int _orderIDHolder = 0;
        private readonly int _id;
        private readonly Dictionary<Menu.Dish, bool> _dishes;
        private int _price;

        public Order(List<Menu.Dish> dishes) {
            _id = _orderIDHolder;
            _orderIDHolder += 1;
            _dishes = new Dictionary<Menu.Dish, bool>();
            foreach (var dish in dishes) {
                try {
                    _dishes.Add(dish, false);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
            foreach (Menu.Dish dish in dishes) {
                _price += dish.Price;
            }
        }

        public Order(Menu.Dish dish) {
            _id = _orderIDHolder;
            _orderIDHolder += 1;
            _dishes = new Dictionary<Menu.Dish, bool>();
            _dishes.Add(dish, false);
            _price += dish.Price;
        }

        public int ID => _id;

        public Dictionary<Menu.Dish, bool> Dishes => _dishes;

        public int Price => _price;


        public void ResetId() {
            _orderIDHolder = 0;
        }

    }
}