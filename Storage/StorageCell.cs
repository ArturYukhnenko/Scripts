using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage {
    public class StorageCell : MonoBehaviour{
        
        public Sprite defaultImg;
        private GameObject _itemGameObject;

        public void SetSpriteForCell(Sprite sprite) {
            if (sprite == null) {
                throw new Exception("Spite is empty");
            }
            _itemGameObject.GetComponent<Image>().sprite = sprite;
        }
        
        public void ResetCell() {
            if (defaultImg == null) {
                throw new Exception("Default image for cell is not set");
            }
            _itemGameObject.GetComponent<Image>().sprite = defaultImg;
            _itemGameObject.GetComponentInChildren<TMP_Text>().text = "Hey";
        }

        public int ID { get; set; }

        public string CellItemName { get; set; }

        public GameObject ItemGameObject {
            get => _itemGameObject;
            set => _itemGameObject = value;
        }
    }
}