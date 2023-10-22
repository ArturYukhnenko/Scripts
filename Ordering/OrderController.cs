using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MenuEquipment.SO;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ordering {
    public class OrderController : MonoBehaviour {

        public event Action<Status> OnStatusChange;
        
        private Status _status = Status.New;
        [SerializeField]
        private GameObject orderItem;
        [SerializeField]
        private GameObject orderItemSpawner;
        [SerializeField]
        private float orderTimeLeft;
        private Order _order;
        public int OrderID => _order.ID;
        
        public ReadOnlyCollection<Menu.Dish> DishesList {
            get {
                List<Menu.Dish> d = new List<Menu.Dish>();
                d.AddRange(_order.Dishes.Keys);
                return new ReadOnlyCollection<Menu.Dish>(d);
            }
        }

        [SerializeField] 
        private GameObject button;
        
        //Timer
        [SerializeField] 
        private TMP_Text timer;

        public Status Status => _status;

        private void Start() {
            StorageController.Instance.OnAddedItems += CheckItemsForOrder;
            StorageController.Instance.OnRemovedItems += CheckItemsForOrder;
        }

        public void SetStatusReady() {
            _status = Status.Ready;
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
            CheckItemsForOrder();
        }

        private void Update() {
            if (_status == Status.Ready) {
                button.SetActive(true);
            }else {
                button.SetActive(false);
            }
            if (!TimeFinished()) {
                orderTimeLeft -= Time.deltaTime;
            }else {
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
                if (StorageController.Instance.IfItemInStorage(dish.Key.Name)) {
                    _order.Dishes[dish.Key] = true;
                    i++;
                }else {
                    _order.Dishes[dish.Key] = false;
                }
            }

            try {
                if (i > 0) {
                    UpdateStatus(Status.InProgress); 
                }
            }catch (Exception e) {
                Console.WriteLine(e + " Current status is: " + _status);
                throw;
            }
            try {
                if (i == _order.Dishes.Count && (_status != Status.New || _status != Status.Finished)) {
                    UpdateStatus(Status.Ready);
                }
            }catch (Exception e) {
                Console.WriteLine(e + " Current status is: " + _status);
                throw;
            }
        }

        public void FinishOrder() {
            if (_status != Status.Ready) {
                throw new Exception("Order cannot be accomplished");
            }

            foreach (Menu.Dish dish in _order.Dishes.Keys.ToList()) {
                StorageController.Instance.GetDishFromStorage(dish.Name);
            }
            
            UpdateStatus(Status.Finished);
            StorageController.Instance.AddEarnedMoney(_order.Price);
            GameObject.FindWithTag("GameManager").GetComponent<OrderManager>().RemoveOrderFromList(this.gameObject);
            Destroy(this.gameObject);
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
            OnStatusChange?.Invoke(_status);
        }
    }

    public enum Status {
        New, InProgress, Ready, Finished
    }
}