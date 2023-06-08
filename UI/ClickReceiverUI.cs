using MenuEquipment;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    public class ClickReceiverUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private GameObject popup;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerId == -1)
            {
                Debug.Log("2d object left click received");
               // MenuController.EditableMode = false;
            
            } 
            if (eventData.pointerId == -2)
            {
                Debug.Log("2d object right click received");
                //MenuController.EditableMode = true;
            }
            var popupObj = Instantiate(popup);
        }
    }
}
