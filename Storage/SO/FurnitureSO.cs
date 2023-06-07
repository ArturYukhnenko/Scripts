using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Exceptions;
using UnityEngine;

namespace Storage.SO {
    [CreateAssetMenu(fileName = "Furniture", menuName = "SO/Storage/Items/Furniture", order = 5)]
    public class FurnitureSO : ScriptableObject {
        public List<Furniture> rawComponents = new List<Furniture>();

        public Furniture GetIngredient(string ingredientName) {
            foreach(Furniture ingredient in rawComponents) {
                if (ingredient.Name.Equals(ingredientName)) {
                    return ingredient;
                }
            }
            throw new ElementNotFoundException("Ingredient not found");
        }
        
        public bool IsIngredientExists(string ingredientName) {
            foreach(Furniture ingredient in rawComponents) {
                if (ingredient.Name.Equals(ingredientName)) {
                    return true;
                }
            }
            return false;
        }

        [Serializable]
        public class Furniture : IItem {
            
            [SerializeField]
            private string ingredientName;
            [SerializeField]
            private Sprite icon;
            [SerializeField]
            private GameObject prefab;
            [SerializeField]
            private Vector2Int size = Vector2Int.one;
            
            [SerializeField]
            private int price;

            public string Name => ingredientName;
            public Sprite Icon => icon;
            public int Price => price;

            public GameObject Prefab => prefab;

            public Vector2Int Size => size;
        }
    }
}

