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
    }
}
