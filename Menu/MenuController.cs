using System;
using System.Collections;
using System.Collections.Generic;
using Storage;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool editableMode;
    [SerializeField]
    private GameObject componentPrefab, spawner, ingredientPrefab;
    [SerializeField] private Menu _menu;
    private Dictionary<string, GameObject> dishVars;
    private ComponentController cc;
    public RawComponents ingredients;
    
    void Start()
    {
        dishVars = new Dictionary<string, GameObject>();
                if (editableMode)
                {Debug.Log("MenuController editable on");
                    foreach (var dish in _menu.dishes)
                    {
                        showDish(dish);
                        cc.Toggle.isOn = dish.activated;
                    }
                }
                else
                {Debug.Log("MenuController editable off");
                    foreach (var dish in _menu.dishes)
                    {
                        if (dish.activated)
                        {
                            showDish(dish);
                            Destroy(cc.Toggle.gameObject);
                        }
                                        
                    }
                }
    }
    void Update()
    {if (editableMode)
                 {
                         foreach (var dish in _menu.dishes)
                         {
                             cc = dishVars[dish.dishName].GetComponent<ComponentController>();
                             dish.activated = cc.Toggle.isOn;
                         }
                                 
                 }
    }

    private void OnDestroy()
    {
        
    }

    private void showDish(Menu.Dish dish)
    {
        GameObject dishVar = Instantiate(componentPrefab, spawner.transform, true);
        cc = dishVar.GetComponent<ComponentController>();
        cc.Title.text = dish.dishName;
        if(editableMode)
            dishVars.Add(dish.dishName, dishVar);
        cc.IngredientsSpawner.name += "_" + dish.dishName;
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
