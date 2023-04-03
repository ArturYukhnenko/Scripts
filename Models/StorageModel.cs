using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models {
    [Serializable]
    public class StorageModel : IModel {

        public List<string> Keys;
        public List<int> Values;
        public int maxCapacity;
        public int coins;

        public IModel Load(string data) {
            return JsonUtility.FromJson<StorageModel>(data);
        }
    }
}