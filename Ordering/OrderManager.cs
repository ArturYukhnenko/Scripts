using System;
using System.Collections.Generic;
using MenuEquipment.SO;
using Storage;
using UnityEngine;

namespace Ordering {
    public class OrderManager : MonoBehaviour {

        [SerializeField] 
        private GameObject spawnPoint;
        [SerializeField] 
        private GameObject orderPrefab;
        private List<GameObject> _existingOrders;

        private void Start() {
            _existingOrders = new List<GameObject>();
        }

        public void CreateOrder(List<Menu.Dish> dishes) {
            GameObject order = Instantiate(orderPrefab, spawnPoint.transform);
            order.GetComponent<OrderController>().Initialize(dishes);

            RectTransform rt = order.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0); 
            rt.localScale = new Vector3(1, 1, 1); 
            order.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);
            _existingOrders.Add(order);
        }

        public void Test() {
            List<Menu.Dish> dish = new List<Menu.Dish>();
            dish.Add(StorageController.Instance.ReceiveActualDishes().GetDish("Latte"));
            CreateOrder(dish);
        }
    }
}