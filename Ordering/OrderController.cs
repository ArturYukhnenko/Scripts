using System;
using System.Collections.Generic;
using System.Linq;
using MenuEquipment.SO;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ordering {
    public class OrderController : MonoBehaviour {

        private Status _status;
        [SerializeField]
        private GameObject orderItem;
        [SerializeField]
        private GameObject orderItemSpawner;
        [SerializeField]
        private float orderTimeLeft;
        private Order _order;

        [SerializeField] 
        private GameObject button;
        
        //Timer
        [SerializeField] 
        private TMP_Text timer;

        private void Start() {
            _status = Status.New;
        }

        public void Initialize(List<Menu.Dish> dishes) {
            if (dishes.Count == 1) {
                _order = new Order(dishes[0]);
            } else { 
                _order = new Order(dishes);
            }
            for (int i = 0; i < dishes.Count; i++) {
                GameObject item = Instantiate(orderItem, orderItemSpawner.transform);
                RectTransform rt = item.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(0, 0, 0);
                rt.localScale = new Vector3(1, 1, 1); 
                item.GetComponentInChildren<Image>().sprite = dishes[i].Icon;
            }
        }

        public Order Test() {
            return _order;
        }

        private void Update() {
            CheckItemsForOrder();

            if (_status == Status.Ready) {
                button.SetActive(true);
            }
            if (!TimeFinished()) {
                orderTimeLeft -= Time.deltaTime;
            }else {
                Destroy(this.gameObject);
            }

            if (_status == Status.Finished) {
                StorageController.Instance.AddEarnedMoney(_order.Price);
                Destroy(this.gameObject);
            }
            UpdateTimer();
        }

        private void UpdateTimer() {
            float minutes = Mathf.FloorToInt(orderTimeLeft / 60);
            float seconds = Mathf.FloorToInt(orderTimeLeft % 60);

            timer.text = $"{minutes:00} : {seconds:00}";
        }

        private bool TimeFinished() {
            if (orderTimeLeft > 0) {
                return false;
            }

            orderTimeLeft = 0;
            return true;
        }

        private void CheckItemsForOrder() {
            int i = 0;
            foreach (var dish in _order.Dishes.ToArray()) {
                if (StorageController.Instance.StoredItems.Any(d => d.Key.Name == dish.Key.Name)) {
                    _order.Dishes[dish.Key] = true;
                    Debug.Log("here");
                    i++;
                }else {
                    _order.Dishes[dish.Key] = false;
                }
            }
            Debug.Log(i);
            try {
                if (i > 0) {
                    UpdateStatus(Status.InProgress); 
                    Debug.Log(i + "In progress");
                }
                if (i == _order.Dishes.Count && (_status != Status.New || _status != Status.Finished)) {
                    UpdateStatus(Status.Ready);
                    Debug.Log(i + "Ready");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }

        public void FinishOrder() {
            if (_status != Status.Ready) {
                throw new Exception("Order cannot be accomplished");
            }

            foreach (KeyValuePair<Menu.Dish,bool> dish in _order.Dishes) {
                StorageController.Instance.GetDishFromStorage(dish.Key.Name);
            }
            
            UpdateStatus(Status.Finished);
        }

        private void UpdateStatus(Status status) {
            switch (_status) {
                case Status.New:
                    if (status == Status.InProgress) {
                        _status = status;
                    } else {
                        throw new Exception("Status cannot be set");
                    }
                    break;
                case Status.InProgress:
                    if (status == Status.Ready) {
                        _status = status;
                    } else {
                        throw new Exception("Status cannot be set");
                    }
                    break;
                case Status.Ready:
                    if (status == Status.InProgress || status == Status.Finished) {
                        _status = status;
                    } else {
                        throw new Exception("Status cannot be set");
                    }
                    break;
                case Status.Finished:
                    throw new Exception("Status cannot be set");
                default:
                    throw new Exception("Status unknown");
            }
        }
    }
}