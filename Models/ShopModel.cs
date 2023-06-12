using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models {
    [Serializable]
    public class ShopModel : IModel {
        
        public List<ShopItemHolder> shopItems;
        
        public IModel Load(string data) {
            return JsonUtility.FromJson<ShopModel>(data);
        }
    }

    [Serializable]
    public class ShopItemHolder {
        public String price;
        public String available;
        public String goodName;
    }
}