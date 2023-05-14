using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage {
    public class StorageCell {
        
        public static Sprite DefaultImg;
        private GameObject _itemGameObject;

        public void ResetCell() {
            if (DefaultImg == null) {
                throw new Exception("Default image for cell is not set");
            }
            _itemGameObject.GetComponent<Image>().sprite = DefaultImg;
            _itemGameObject.GetComponentInChildren<TMP_Text>().text = "";
        }

        public int ID { get; set; }

        public string CellItemName { get; set; }

        public Sprite DefaultImg {
            get => _defaultImg;
            set => _defaultImg = value;
        }

        public GameObject ItemGameObject {
            get => _itemGameObject;
            set => _itemGameObject = value;
        }
    }
}