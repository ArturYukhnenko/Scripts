using System;
using System.Collections.Generic;
using Exceptions;
using UnityEngine;

namespace UI.SO {
    [CreateAssetMenu(fileName = "PopupsContainer", menuName = "SO/Popups/PopupCreator", order = 1)]
    public class PopupsContainer : ScriptableObject {
        
        [SerializeField]
        private List<PopupData> _data = new List<PopupData>();
        
        public PopupData GetPopup(string popupName) {
            foreach(PopupData popupData in _data) {
                if (popupData.name.Equals(popupName)) {
                    return popupData;
                }
            }
            throw new ElementNotFoundException("Popup not found");
        }

        [Serializable]
        public class PopupData {

            public string name;
            public GameObject popup;

        }

    }
}