using System;
using System.Collections.Generic;

namespace Storage {
    [Serializable]
    public class StorageDataSaver {

        public List<string> Keys;
        public List<int> Values;
        public int maxCapacity;
        public int coins;

    }
}