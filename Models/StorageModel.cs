using System;
using System.Collections.Generic;

namespace Models {
    [Serializable]
    public class StorageModel : IModel {

        public List<string> Keys;
        public List<int> Values;
        public int maxCapacity;
        public int coins;

    }
}