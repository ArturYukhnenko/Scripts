using System.Collections.Generic;
using UnityEngine;

namespace MenuEquipment.SO {
    [CreateAssetMenu(fileName = "Equipment", menuName = "SO/Menu", order = 4)]
    public class EquipmentData : ScriptableObject
    {
        public string toolName;
        public List<string> dishes;
    
    }
}
