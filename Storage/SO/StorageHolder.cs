using System.Collections.Generic;
using UnityEngine;

namespace Storage.SO {
    [CreateAssetMenu(fileName = "DataBase", menuName = "SO/Storage/DataBase", order = 2)]
    public class StorageHolder : ScriptableObject {

        private Storage _storage;
        [SerializeField] private int maxCapacity;
        [SerializeField] private int startIncome;

        public void CreateStorage() {
            _storage = new Storage(maxCapacity,startIncome);
        }

        public void AssignStorage(int maximumCapacity, int currentMoney, Dictionary<IItem, int> storedItems) {
            _storage = new Storage(maximumCapacity, currentMoney, storedItems);
        }

        public Storage Storage {
            get => _storage;
            private set => _storage = value;
        }
    }
}