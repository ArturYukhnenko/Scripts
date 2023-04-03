using System.Collections.Generic;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuEquipment {
    public class ComponentController : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private GameObject ingredientPrefab;
        [SerializeField] private Toggle toggle;
        [SerializeField] private GameObject ingredientsSpawner;
        private List<string> _ingredients = new List<string>();

        public TMP_Text Title
        {
            get => title;
            set => title = value;
        }

        public GameObject IngredientPrefab
        {
            get => ingredientPrefab;
            set => ingredientPrefab = value;
        }

        public Toggle Toggle
        {
            get => toggle;
            set => toggle = value;
        }

        public GameObject IngredientsSpawner
        {
            get => ingredientsSpawner;
            set => ingredientsSpawner = value;
        }

        public void AddIngerdient(string ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void Cook()
        {
            foreach (var ingredient in _ingredients)
            {
                StorageController.Instance.UseItemFromStorage(ingredient, 1);
               // StorageController.Instance. add dish to storage
            }
            
        }
    }
}
