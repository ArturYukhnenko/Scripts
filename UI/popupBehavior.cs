using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class popupBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject popup;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyPopup()
    {
        Destroy(popup);
    }
    
}
