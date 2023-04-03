using MenuEquipment.SO;
using Storage.SO;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace MenuEquipment {
    public class EquipmentPopupBehaviour : PopupBehavior
    {
        //[SerializeField]
        private EquipmentData _equipmentData;

        public TMP_Text titlePopup;
        public GameObject dishPrefab;
        public GameObject ingredientPrefab;
        public Menu menu;
        public RawComponents ingredients;
        void Start()
        {
            titlePopup.text = _equipmentData.toolName;
            foreach (var dishName in _equipmentData.dishes)
            {
                Menu.Dish dish = menu.dishes.Find(i => i.dishName == dishName);
                var dishVar = Instantiate(dishPrefab);
                dishVar.transform.SetParent(GameObject.Find("menuVariants").transform);
                ComponentController cc = dishVar.GetComponent<ComponentController>();
                cc.Title.text = dishName;
                cc.IngredientsSpawner.name += "_" + dishName;
                Debug.Log(dishName + "ingredients: " + dish.ingredients.ToString());
                foreach (var ingredient in dish.ingredients)
                {
                    Debug.Log(ingredient);
                    var ingredientVar = Instantiate(ingredientPrefab);
                    ingredientVar.transform.SetParent(GameObject.Find(cc.IngredientsSpawner.name).transform);
                    var iconIngredient = ingredientVar.GetComponent<Image>();
                    iconIngredient.sprite = ingredients.GetIngredient(ingredient).Icon;
                    ingredientVar.GetComponentInChildren<TMP_Text>().text = " ";
                }
           
            }
        }

        public EquipmentData EquipmentData
        {
            get => _equipmentData;
            set => _equipmentData = value;
        }
    }
}
