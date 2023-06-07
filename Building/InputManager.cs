using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;

    private Vector3 lastPosition;

    [SerializeField] private LayerMask ground;

    
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ground))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
