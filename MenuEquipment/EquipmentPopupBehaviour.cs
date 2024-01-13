using System;
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
            try
            {
                titlePopup.text = _equipmentData.toolName;
                    foreach (var dishName in _equipmentData.dishes) {
                        Menu.Dish dish = menu.GetDish(dishName);
                        if (dish.activated)
                        {
                            var dishVar = Instantiate(dishPrefab);
                            dishVar.transform.SetParent(GameObject.Find("menuVariants").transform);
                            ComponentController cc = dishVar.GetComponent<ComponentController>();
                            cc.Title.text = dishName;
                            cc.Image.sprite = dish.Icon;
                            cc.IngredientsSpawner.name += "_" + dishName;
                            Debug.Log(dishName + "ingredients: " + dish.ingredients.ToString());
                            foreach (var ingredient in dish.ingredients)
                            {
                                cc.AddIngerdient(ingredient);
                                //Debug.Log(ingredient);
                                var ingredientVar = Instantiate(ingredientPrefab);
                                ingredientVar.transform.SetParent(GameObject.Find(cc.IngredientsSpawner.name).transform);
                                var iconIngredient = ingredientVar.GetComponent<Image>();
                                iconIngredient.sprite = ingredients.GetIngredient(ingredient).Icon;
                            }
                        }
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public EquipmentData EquipmentData
        {
            get => _equipmentData;
            set => _equipmentData = value;
        }
    }
}
