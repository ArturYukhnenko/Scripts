using System.Collections.Generic;
using MenuEquipment.SO;

namespace Ordering {
    public class Order {

        private static int _orderID;
        private List<Menu.Dish> _dishes;
        private int _price;

        public Order(List<Menu.Dish> dishes) {
            _orderID += 1;
            _dishes = dishes;
            foreach (Menu.Dish dish in dishes) {
                _price += dish.Price;
            }
        }

        public Order(Menu.Dish dish) {
            _orderID += 1;
            _dishes = new List<Menu.Dish>();
            _dishes.Add(dish);
            _price += dish.Price;
        }


        public static int OrderID => _orderID;

        public List<Menu.Dish> Dishes => _dishes;

        public int Price => _price;


        public void ResetId() {
            _orderID = 0;
        }

    }
}