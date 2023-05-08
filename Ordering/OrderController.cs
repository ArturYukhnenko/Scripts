using System.Collections.Generic;
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
                item.GetComponentInChildren<RectTransform>().localPosition = new Vector3(1, 1, 1);
                item.GetComponentInChildren<Image>().sprite = dishes[i].Icon;
            }
        }

        private void Update() {
            
            if (!TimeFinished()) {
                orderTimeLeft -= Time.deltaTime;
            }else {
                if (_status != Status.Finished) {
                    Destroy(this);
                }
            }

            if (_status == Status.Finished) {
                StorageController.Instance.AddEarnedMoney(_order.Price);
                Destroy(this);
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
    }
}