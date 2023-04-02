using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;
using UnityEngine.UI;

public class ComponentDisplay : MonoBehaviour
{
    public RawComponents components;

    public Image imageObject;
    public new string name;
    void Start() {
        imageObject.sprite = components.GetIngredient(name).Icon;
    }

}
