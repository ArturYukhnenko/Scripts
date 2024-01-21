using MenuEquipment.SO;
using Storage.SO;
using Unity.VisualScripting;
using UnityEngine;

namespace Storage {
    public class DataHolder : MonoBehaviour{
        
        //Stored Items
        [SerializeField]
        private StorageHolder storageHolder;
        [SerializeField]
        private RawComponents rawComponents;
        [SerializeField]
        private Menu dishes;

        public void InvokeStartLoading() {
            if (StorageController.Instance != null) {
                if(!StorageController.Instance.IsSet){
                    StorageController.Instance.SetFields(storageHolder,rawComponents,dishes);
                }
            }
            //Destroy(this.GameObject());
        }
    }
}