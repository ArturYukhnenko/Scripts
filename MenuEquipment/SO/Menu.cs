using System;
using System.Collections.Generic;
using Storage;
using UnityEngine;

namespace MenuEquipment.SO {
    [CreateAssetMenu(fileName = "Menu", menuName = "SO/Menu", order = 3)]
    public class Menu : ScriptableObject
    {
        public List<Dish> dishes = new List<Dish>();
        
        [Serializable]
        public class Dish : IItem
        {
            public string dishName;
            public bool activated;
            public List<string> ingredients;
            public string Name { get; set; }
            public Sprite Icon { get; set; }
        }

    

    }
}
