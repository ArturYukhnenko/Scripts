using System;
using System.Collections;
using System.Collections.Generic;
using Storage.SO;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator, cellIndicator;
    [SerializeField] private FurnitureSO _furnitureSo;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;
    private string _selectedItem;


    void Start()
    {
    }

    private void Update()
    {
        Debug.Log("upd one item" + _selectedItem);
        if (_selectedItem is null)
            return;
        Debug.Log("upd");
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        GameObject newFurniture = Instantiate(_furnitureSo.GetIngredient(_selectedItem).Prefab);
        newFurniture.transform.position = _grid.CellToWorld(gridPosition);
        
        
    }
    
    public void StartPlacement(string itemName)
    {
        //StopPlacement();
        if (!_furnitureSo.IsIngredientExists(itemName))
        {
            Debug.LogError($"Furniture not found: {itemName}");
            return;
        }

        _selectedItem = itemName;
        
        cellIndicator.SetActive(true);
        PlaceStructure();
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
       
        Debug.Log(_selectedItem + " + " + itemName);
            
    }

    private void StopPlacement()
    {
        
        _selectedItem = " ";
        cellIndicator.SetActive(false);
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
        
       
        //mouseIndicator.transform.position = new Vector3(cellIndicator.transform.position.x + 0.4f, mouseIndicator.transform.position.y,
          //  cellIndicator.transform.position.z + 0.55f) ;
    }

    private void PlaceStructure()
    {
        if (_inputManager.IsPointerOverUI()) 
            return;
        Debug.Log("placeStucture log");
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        GameObject newFurniture = Instantiate(_furnitureSo.GetIngredient(_selectedItem).Prefab);
        newFurniture.transform.position = _grid.CellToWorld(gridPosition);
    }
    
    
}
