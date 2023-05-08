using System;
using System.Collections.Generic;
using MenuEquipment.SO;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Utils {
    public class CafeSceneEventManager{

        public static UnityEvent<List<Menu.Dish>> OnOrderCreated = new UnityEvent<List<Menu.Dish>>();

        public static void OrderCreated(List<Menu.Dish> dishes) {
            if(OnOrderCreated != null) OnOrderCreated.Invoke(dishes);
        }
    }
}