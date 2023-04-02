using System.Collections;
using System.Collections.Generic;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPopupBehaviour : popupBehavior
{
    //[SerializeField]
    private EquipmentPopup _equipmentPopup;

    public TMP_Text name;
    public GameObject variantPrefab;
    public GameObject ingredientPrefab;
    public Menu menu;
    public RawComponents ingredients;
    void Start()
    {
        name.text = _equipmentPopup.toolName;
        foreach (var dishName in _equipmentPopup.dishes)
        {
            Menu.Dish dish = menu.dishes.Find(i => i.dishName == dishName);
            var dishVar = Instantiate(variantPrefab);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public EquipmentPopup EquipmentPopup
    {
        get => _equipmentPopup;
        set => _equipmentPopup = value;
    }
}
