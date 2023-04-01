using System;
using System.Collections;
using System.Collections.Generic;
using Storage;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "SO/menu", order = 54)]
public class Menu : ScriptableObject
{
    public List<Dish> dishes = new List<Dish>();
    [Serializable]
    public class Dish : IIngredient
    {
        public string dishName;
        public bool activated;
        public List<string> ingredients;
    }

    

}
