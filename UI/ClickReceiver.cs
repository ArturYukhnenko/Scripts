using MenuEquipment;
using MenuEquipment.SO;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    public class ClickReceiver : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject popup;
        [SerializeField]
        private EquipmentData equipmentData;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (CafeManager.IsDay)
            {
                Debug.Log("3d object left click received");
                var popupObj = Instantiate(popup);
                var script = popupObj.GetComponent<EquipmentPopupBehaviour>();
                script.EquipmentData = equipmentData;
            } 
            if (eventData.pointerId == -2)
            {
                Debug.Log("3d object right click received");
            }
        
        }
    }
}
