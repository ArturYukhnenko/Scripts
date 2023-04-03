using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Storage;
using UnityEngine;

namespace MenuEquipment.SO {
    [CreateAssetMenu(fileName = "Menu", menuName = "SO/Menu", order = 3)]
    public class Menu : ScriptableObject
    {
        public List<Dish> dishes = new List<Dish>();
        
        public Dish GetDish(string dishName) {
            foreach(Dish dish in dishes) {
                if (dish.Name.Equals(dishName)) {
                    return dish;
                }
            }
            throw new Exception("Dish not found");
        }
        
        public bool IsDishExists(string dishName) {
            foreach(Dish ingredient in dishes) {
                if (ingredient.Name.Equals(dishName)) {
                    return true;
                }
            }
            return false;
        }

        [Serializable]
        public class Dish : IItem
        {
            [SerializeField]
            private string dishName;
            [SerializeField]
            private Sprite icon;
            public bool activated;
            public List<string> ingredients;
            public string Name => dishName;
            public Sprite Icon => icon;
        }

    

    }
}