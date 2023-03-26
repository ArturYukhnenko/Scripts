using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static bool editableMode;
    [SerializeField]
    private GameObject componentPrefab;

    [SerializeField] private Menu _menu;
    private Dictionary<string, GameObject> dishVars;
    private ComponentController cc;
    void Start()
    {
        dishVars = new Dictionary<string, GameObject>();
                if (editableMode)
                {Debug.Log("MenuController editable on");
                    foreach (var dish in _menu.dishes)
                    {
                        GameObject dishVar = Instantiate(componentPrefab, GameObject.Find("menuVariants").transform, true);
                        cc = dishVar.GetComponent<ComponentController>();
                        cc.Title.text = dish.dishName;
                        cc.Toggle.isOn = dish.activated;
                        Debug.Log(dish.dishName + dishVar);
                        dishVars.Add(dish.dishName, dishVar);
                    }
                }
                else
                {Debug.Log("MenuController editable off");
                    foreach (var dish in _menu.dishes)
                    {
                        if (dish.activated)
                        {
                            GameObject dishVar = Instantiate(componentPrefab, GameObject.Find("menuVariants").transform, true);
                            cc = dishVar.GetComponent<ComponentController>();
                            cc.Title.text = dish.dishName;
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
}
