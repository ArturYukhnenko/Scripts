using System.Collections.Generic;
using MenuEquipment.SO;
using Storage;
using Storage.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuEquipment {
    public class MenuController : MonoBehaviour
    {
        public static bool EditableMode;
        [SerializeField]
        private GameObject dishPrefab, spawner, ingredientPrefab;
        [SerializeField] private Menu menu;
        //wtrfack
        private Dictionary<string, GameObject> _dishVariants;
        private ComponentController _cc;
        public RawComponents ingredients;
    
        void Start()
        {
            _dishVariants = new Dictionary<string, GameObject>();
            if (EditableMode)
            {Debug.Log("MenuController editable on");
                foreach (var dish in menu.dishes)
                {
                    ShowDish(dish);
                    _cc.Toggle.isOn = dish.activated;
                }
            }
            else
            {Debug.Log("MenuController editable off");
                foreach (var dish in menu.dishes)
                {
                    if (dish.activated)
                    {
                        ShowDish(dish);
                        Destroy(_cc.Toggle.gameObject);
                    }
                                        
                }
            }
        }
        void Update()
        {if (EditableMode)
            {
                foreach (var dish in menu.dishes)
                {
                    //Too heave method. Make it on Exit/Destroy
                    _cc = _dishVariants[dish.Name].GetComponent<ComponentController>();
                    dish.activated = _cc.Toggle.isOn;
                }
                                 
            }
        }

        private void OnDestroy()
        {
        
        }

        private void ShowDish(Menu.Dish dish)
        {
            GameObject dishVar = Instantiate(dishPrefab, spawner.transform, true);
            _cc = dishVar.GetComponent<ComponentController>();
            _cc.Title.text = dish.Name;
            if(EditableMode)
                _dishVariants.Add(dish.Name, dishVar);
            _cc.IngredientsSpawner.name += "_" + dish.Name;
            foreach (var ingredient in dish.ingredients)
            {
                Debug.Log(ingredient);
                var ingredientVar = Instantiate(ingredientPrefab);
                ingredientVar.transform.SetParent(GameObject.Find(_cc.IngredientsSpawner.name).transform);
                var iconIngredient = ingredientVar.GetComponent<Image>();
                iconIngredient.sprite = ingredients.GetIngredient(ingredient).Icon;
                ingredientVar.GetComponentInChildren<TMP_Text>().text = "";
            }
        }
    }
}
