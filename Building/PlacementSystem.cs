using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;
    private bool mousePressed;
    
    // Start is called before the first frame update
    void Start()
    {
        mousePressed = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!mousePressed)
        {
            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
                    Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
                    mouseIndicator.transform.position = mousePosition;
                    cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        }

    }

    private void SetToCenter()
    {
        mouseIndicator.transform.position = new Vector3(cellIndicator.transform.position.x + 0.4f, mouseIndicator.transform.position.y, cellIndicator.transform.position.z + 0.55f) ;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mousePressed = true;
        SetToCenter();
        Debug.Log("pressed");
    }
}
