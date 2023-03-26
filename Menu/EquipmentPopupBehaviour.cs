using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentPopupBehaviour : popupBehavior
{
    //[SerializeField]
    private EquipmentPopup _equipmentPopup;

    public TMP_Text name;
    public GameObject variantPrefab;
    public GameObject ingredientPrefab;
    
    void Start()
    {
        name.text = _equipmentPopup.toolName;
        foreach (var dish in _equipmentPopup.dishes)
        {
            var dishVar = Instantiate(variantPrefab);
            dishVar.transform.parent = GameObject.Find("menuVariants").transform;
            ComponentController cc = dishVar.GetComponent<ComponentController>();
            cc.Title.text = dish;
/*
            foreach (var ingredient in dish.ingredients)
            {
                var ingredientVar = Instantiate(ingredientPrefab);
                ingredientVar.transform.parent = GameObject.Find("ingredients").transform;
                ComponentDisplay cd = ingredientVar.GetComponent<ComponentDisplay>();
                cd.name = ingredient;
            }
         */   
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
