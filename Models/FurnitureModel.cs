using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models {
    [Serializable]
    public class FurnitureModel : IModel {

        //grid x*y walls and floor
        public int roomSize_x;
        public int roomSize_y;
        
        public List<CoordinatesSaver> CoordinatesFurniture;
        

        public IModel Load(string data) {
            return JsonUtility.FromJson<FurnitureModel>(data);
        }
    }
}