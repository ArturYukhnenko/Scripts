using System;
using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Cook(List<string> ingredients, string dish)
    {
        foreach (var ingredient in ingredients)
        {
                
            StorageController.Instance.GetIngredientFromStorage(ingredient);
        }
        StorageController.Instance.AddDishToStorage(dish);
        Debug.Log("Cooked " + dish);
        
    }
}
