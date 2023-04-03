using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Storage.SO {
    [CreateAssetMenu(fileName = "RawIngredients", menuName = "SO/Storage/Items/Ingredients", order = 1)]
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
        
        public bool IsIngredientExists(string ingredientName) {
            foreach(RawIngredient ingredient in rawComponents) {
                if (ingredient.Name.Equals(ingredientName)) {
                    return true;
                }
            }
            return false;
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

