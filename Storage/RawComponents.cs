using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Storage {
    [CreateAssetMenu(fileName = "RawComponents", menuName = "SO/Raw", order = 51)]
    public class RawComponents : ScriptableObject {
        public List<RawIngredient> rawComponents = new List<RawIngredient>();

        public RawIngredient GetIngredient(string ingredientName) {
            foreach(RawIngredient ingredient in rawComponents) {
                if (ingredient.ingredientName.Equals(ingredientName)) {
                    return ingredient;
                }
                
            }

            throw new Exception("Ingredient not found");
        }

        [Serializable]
        public class RawIngredient : IIngredient {
            public string ingredientName;
            public Sprite icon;
            public float price;
        }
    }
}

