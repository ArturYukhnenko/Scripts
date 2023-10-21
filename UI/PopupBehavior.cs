using UnityEngine;

namespace UI {
    public class PopupBehavior : MonoBehaviour
    {
        [SerializeField]
        private GameObject closeButton;
        [SerializeField]
        private GameObject currentPopup;
    
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void DestroyPopup()
        {
            Destroy(currentPopup);
        }
    
    }
}
