using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    private Vector3 lastPosition;
    [SerializeField] private LayerMask ground;
    public event Action OnClicked, OnExit;
    

    void Start()
    {
        sceneCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("updateClick");
            OnClicked?.Invoke();
        }
            
        if (Input.GetMouseButtonDown(1))
            OnExit?.Invoke();
    }

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
    

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
}
