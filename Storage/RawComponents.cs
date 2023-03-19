using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Storage
{
    [CreateAssetMenu(fileName = "RawComponents", menuName = "SO/Raw", order = 51)]
    public class RawComponents : ScriptableObject {
        public List<Component> rawComponents = new List<Component>();
        
        
        
        [Serializable]
        public class Component {
            [SerializeField]
            private string Name
            {
                get
                {
                    return Name;
                }
            }

            [SerializeField]
            private Sprite Icon
            {
                get
                {
                    return Icon;
                }
            }

            [SerializeField]
            private float Price
            {
                get
                {
                    return Price;
                }
            }
        }
    }
}

