using Storage.SO;
using UnityEngine;
using UnityEngine.UI;

namespace MenuEquipment {
    public class ComponentDisplay : MonoBehaviour
    {
        public RawComponents components;

        public Image imageObject;
        public new string name;
        
        void Start() {
            imageObject.sprite = components.GetIngredient(name).Icon;
        }

    }
}
