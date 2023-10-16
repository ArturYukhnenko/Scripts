using System;
using System.Collections;
using System.Collections.Generic;
using Storage.SO;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject  cellIndicator;
    [SerializeField] private FurnitureSO _furnitureSo;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;
    private string _selectedItem;
    private GameObject flyingFurniture;


    void Start()
    {
    }

    private void Update()
    {
        //Debug.Log("upd one item" + _selectedItem);
        if (_selectedItem != null )
        {
           // Destroy(flyingFurniture);
            Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
           // flyingFurniture = Instantiate(_furnitureSo.GetIngredient(_selectedItem).Prefab);
            flyingFurniture.transform.position =
                        new Vector3(_grid.CellToWorld(gridPosition).x, 0, _grid.CellToWorld(gridPosition).z);
            
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    flyingFurniture.transform.Rotate(0,90,0);
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    flyingFurniture.transform.Rotate(0,-90,0);

            }
            
            
        }
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
        _inputManager.OnClicked += PlaceStructure;
        _inputManager.OnExit += StopPlacement;
        
        flyingFurniture = Instantiate(_furnitureSo.GetIngredient(_selectedItem).Prefab);
        Debug.Log("startStucture log");
    }

    private void StopPlacement()
    {
        
        _selectedItem = " ";
        cellIndicator.SetActive(false);
        _inputManager.OnClicked -= PlaceStructure;
        _inputManager.OnExit -= StopPlacement;
        
       
        //mouseIndicator.transform.position = new Vector3(cellIndicator.transform.position.x + 0.4f, mouseIndicator.transform.position.y,
         //   cellIndicator.transform.position.z + 0.55f) ;
    }

    private void PlaceStructure()
    {
       
        Debug.Log("placeStucture log");
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
        GameObject finalFurniture = Instantiate(_furnitureSo.GetIngredient(_selectedItem).Prefab);
        finalFurniture.transform.position = new Vector3(_grid.CellToWorld(gridPosition).x, 0.1f, _grid.CellToWorld(gridPosition).z);
        Destroy(flyingFurniture);
        Destroy(GameObject.Find("BuildingSystem(Clone)"));
    }
    
    
}
