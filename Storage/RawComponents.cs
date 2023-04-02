using System;
using System.Collections.Generic;
using UnityEngine;

namespace Storage {
    [CreateAssetMenu(fileName = "RawIngredients", menuName = "SO/Storage/Items/Ingredients", order = 51)]
    public class RawComponents : ScriptableObject {
        public List<RawIngredient> rawComponents = new List<RawIngredient>();

        public RawIngredient GetIngredient(string ingredientName) {
            foreach(RawIngredient ingredient in rawComponents) {
                if (ingredient.Name.Equals(ingredientName)) {
                    return ingredient;
                }
                
            }

            throw new Exception("Ingredient not found");
        }

        [Serializable]
        public class RawIngredient : IItem {
            
            [SerializeField]
            private string ingredientName;
            [SerializeField]
            private Sprite icon;
            [SerializeField]
            private float price;

            public string Name => ingredientName;
            public Sprite Icon => icon;
            public float Price => price;
        }
    }
}

