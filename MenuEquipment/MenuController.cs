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
        [SerializeField]
        private GameObject dishPrefab, spawner, ingredientPrefab;
        [SerializeField] private Menu menu;
        private Dictionary<string, GameObject> _dishVariants;
        private ComponentController _cc;
        public RawComponents ingredients;
    
        void Start()
        {
            _dishVariants = new Dictionary<string, GameObject>();
            if (!CafeManager.IsDay) 
            {
                Debug.Log("MenuController editable on");
                foreach (var dish in menu.dishes)
                {
                    if (RoomGenerator.FurnitureExist(dish.Instrument()))
                    {
                        ShowDish(dish);
                        _cc.Toggle.isOn = dish.activated;
                    }
                    else
                    {
                        Debug.Log(dish.Instrument());
                        dish.activated = false;
                    }
                    //Debug.Log("Instrument "+dish.Instrument());

                }
            }
            else
            {
                Debug.Log("MenuController editable off");
                foreach (var dish in menu.dishes)
                {
                    if (dish.activated)
                    {
                        if (RoomGenerator.FurnitureExist(dish.Instrument()))
                        {
                            ShowDish(dish);
                            Destroy(_cc.Toggle.gameObject);
                        }
                    }
                                        
                }
            }
        }
        void Update()
        {
        }

        private void OnDestroy()
        {
            {if (!CafeManager.IsDay)
                {
                    foreach (var dish in menu.dishes)
                    {
                        if (RoomGenerator.FurnitureExist(dish.Instrument()))
                        {
                            _cc = _dishVariants[dish.Name].GetComponent<ComponentController>();
                            dish.activated = _cc.Toggle.isOn;
                        }
                    }
                                 
                }
            }
        }

        private void ShowDish(Menu.Dish dish)
        {
            GameObject dishVar = Instantiate(dishPrefab, spawner.transform, true);
            _cc = dishVar.GetComponent<ComponentController>();
            _cc.Title.text = dish.Name;
            _cc.Image.sprite = dish.Icon;
            if(!CafeManager.IsDay)
                _dishVariants.Add(dish.Name, dishVar);
            _cc.IngredientsSpawner.name += "_" + dish.Name;
            foreach (var ingredient in dish.ingredients)
            {
                var ingredientVar = Instantiate(ingredientPrefab);
                ingredientVar.transform.SetParent(GameObject.Find(_cc.IngredientsSpawner.name).transform);
                var iconIngredient = ingredientVar.GetComponent<Image>();
                iconIngredient.sprite = ingredients.GetIngredient(ingredient).Icon;
                Destroy(ingredientVar.GetComponentInChildren<TMP_Text>());
            }
        }
    }
}
