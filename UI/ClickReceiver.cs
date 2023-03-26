using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject popup;
    [SerializeField]
    private EquipmentPopup PopupData;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            Debug.Log("3d object left click received");
            var popupObj = Instantiate(popup);
            var script = popupObj.GetComponent<EquipmentPopupBehaviour>();
            script.EquipmentPopup = PopupData;
            //popup.gameObject.SetActive(true);
        } 
        if (eventData.pointerId == -2)
        {
            Debug.Log("3d object right click received");
            popup.gameObject.SetActive(true);
        }
        
    }
}
