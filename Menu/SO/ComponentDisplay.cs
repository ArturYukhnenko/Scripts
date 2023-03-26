using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;
using UnityEngine.UI;

public class ComponentDisplay : MonoBehaviour
{
    public RawComponents _components;

    public Image imageObject;
    public string name;
    void Start()
    {
        foreach (var component in _components.rawComponents)
        {
            if (component.ingredientName == name)
            {
                imageObject.sprite = component.icon;
                
            }
        }
    }

}
