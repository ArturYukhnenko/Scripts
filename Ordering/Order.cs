using System.Collections.Generic;
using MenuEquipment.SO;

namespace Ordering {
    public class Order {

        private static int _orderID;
        private Dictionary<Menu.Dish, bool> _dishes;
        private int _price;

        public Order(List<Menu.Dish> dishes) {
            _orderID += 1;
            _dishes = new Dictionary<Menu.Dish, bool>();
            foreach (var dish in dishes) {
                _dishes.Add(dish, false);
            }
            foreach (Menu.Dish dish in dishes) {
                _price += dish.Price;
            }
        }

        public Order(Menu.Dish dish) {
            _orderID += 1;
            _dishes = new Dictionary<Menu.Dish, bool>();
            _dishes.Add(dish, false); 
            _price += dish.Price;
        }

        public static int OrderID => _orderID;

        public Dictionary<Menu.Dish, bool> Dishes => _dishes;

        public int Price => _price;


        public void ResetId() {
            _orderID = 0;
        }

    }
}