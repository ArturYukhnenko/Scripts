using System;
using System.Collections.Generic;
using Exceptions;
using MenuEquipment.SO;
using Storage;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

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

        public OrderController CreateOrder(List<Menu.Dish> dishes) {
            if (_existingOrders.Count >= 5) {
                throw new NotEnoughSpaceException("Cannot accept more orders");
            }
            GameObject order = Instantiate(orderPrefab, spawnPoint.transform);
            order.GetComponent<OrderController>().Initialize(dishes);
            RectTransform rt = order.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0); 
            rt.localScale = new Vector3(1, 1, 1); 
            order.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);
            _existingOrders.Add(order);
            return order.GetComponent<OrderController>();
        }

        public void RemoveOrderFromList(GameObject order) {
            if(_existingOrders.Contains(order))
                _existingOrders.Remove(order);
        }
        
        public void ClearOrdersList() {
            if(_existingOrders.Count > 0)
                _existingOrders.Clear();
        }

        public void Test() {
            List<Menu.Dish> dishes = new List<Menu.Dish>();
            List<Menu.Dish> availableDishes = new List<Menu.Dish>(StorageController.Instance.ReceiveActualDishes());
            int randomAmount =  new Random().Next(1,availableDishes.Count);
            for (int i = 0; i < randomAmount; i++) {
                int randomDish =  new Random().Next(0,availableDishes.Count);
                dishes.Add(availableDishes[randomDish]);
            }
            CreateOrder(dishes);
        }
        
    }
}