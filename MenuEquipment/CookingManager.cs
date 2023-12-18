using System;
using System.Collections;
using System.Collections.Generic;
using Exceptions;
using Storage;
using UnityEngine;

public class CookingManager : MonoBehaviour

{

    
    [SerializeField] private GameObject popupException;
    private static GameObject popupCurrentException;
    private void Start()
    {
        popupCurrentException = popupException;
    }

    public static void Cook(List<string> ingredients, string dish)
    {
        try
        {
            StorageController.Instance.GetIngredientFromStorage(ingredients);

            StorageController.Instance.AddDishToStorage(dish);
            Debug.Log("Cooked " + dish);
        }
        catch(NotEnoughItemsException ex)
        {
            Debug.LogWarning(ex);
            Instantiate(popupCurrentException);
        }
        catch(ElementNotFoundException ex)
        {
            Debug.LogWarning(ex);
            Instantiate(popupCurrentException);
        }
    }
}
