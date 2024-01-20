using System.Collections;
using System.Collections.Generic;
using Models;
using Shop;
using Storage.SO;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private static int x = 18;

    private static int y = 10;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject floorPrefab;
    private List<GameObject> generatedObjects = new List<GameObject>();
    [SerializeField] private FurnitureSO _furnitureSo;
    [SerializeField]private static List<CoordinatesSaver> _furnitures = new List<CoordinatesSaver>();
    //objects of wall(2,3,4,5), (8,7,6,5,4)
    [SerializeField]private List<GameObject> xWallsAdding = new List<GameObject>();
    [SerializeField]private List<GameObject> yWallsAdding = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
       // Load((FurnitureModel)SaveAndLoad.SaveAndLoad.Load("/Room", "RoomData",
       //     ModelTypesEnums.FurnitureModel));
        GenerateWalls();
        GenerateFloor();
    }
    private void GenerateWalls()
    {
        //front side
        var frontFirstWall = GameObject.Find("WallPart (1)").transform;
        for (int i = 1; i < x-1; i++)
        {
            var wall = Instantiate(wallPrefab);
            wall.name = "WallPartFront" + i;
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 frontWallVector3 = new Vector3(frontFirstWall.transform.position.x + i, frontFirstWall.position.y,
                frontFirstWall.position.z);
            wall.transform.SetPositionAndRotation(frontWallVector3, frontFirstWall.rotation);
            wall.transform.localScale = new Vector3(1, 0.25f, 1);
            generatedObjects.Add(wall);
        }
        //back side
        var backFirstWall = GameObject.Find("WallPart (6)").transform;
        for (int i = 1; i < x-1; i++)
        {
            var wall = Instantiate(wallPrefab);
            wall.name = "WallPartBack" + i;
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 backWallVector3 = new Vector3(backFirstWall.transform.position.x + i, backFirstWall.position.y,
                backFirstWall.position.z);
            wall.transform.SetPositionAndRotation(backWallVector3, backFirstWall.rotation);
            generatedObjects.Add(wall);
        }
        //left side
        var leftFirstWall = GameObject.Find("WallPart").transform;
        for (int i = 1; i < y-1; i++)
        {
            var wall = Instantiate(wallPrefab);
            wall.name = "WallPartLeft" + i;
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 leftWallVector3 = new Vector3(leftFirstWall.transform.position.x , leftFirstWall.position.y ,
                leftFirstWall.position.z+ i);
            wall.transform.SetPositionAndRotation(leftWallVector3, leftFirstWall.rotation);
            generatedObjects.Add(wall);
        }
        //right side
        var rightFirstWall = GameObject.Find("WallPart (3)").transform;
        for (int i = 1; i < y-1; i++)
        {
            var wall = Instantiate(wallPrefab);
            wall.name = "WallPartRight" + i;
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 rightWallVector3 = new Vector3(rightFirstWall.transform.position.x , rightFirstWall.position.y ,
                rightFirstWall.position.z+ i);
            wall.transform.SetPositionAndRotation(rightWallVector3, rightFirstWall.rotation);
            generatedObjects.Add(wall);
        }
        Destroy(GameObject.Find("WallPartLeft4"));
        Destroy(GameObject.Find("WallPartLeft5"));
        Destroy(GameObject.Find("WallPartLeft6"));
    }

    private void GenerateFloor()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var floor = Instantiate(floorPrefab);
                floor.transform.SetParent(GameObject.Find("floor").transform);
                Vector3 floorVector3 = new Vector3(floorPrefab.transform.position.x + i, floorPrefab.transform.position.y,
                    floorPrefab.transform.position.z + j);
                floor.transform.SetPositionAndRotation(floorVector3, floorPrefab.transform.rotation);
                generatedObjects.Add(floor);
            }
        }
        
    }

    public void AddArea()
    {
        x += 1;
        y += 1;
        foreach (var wall in xWallsAdding)
        {
            var pos = wall.transform.position;
            wall.transform.position = new Vector3(pos.x+1, pos.y, pos.z);
        }
        foreach (var wall in yWallsAdding)
        {
            var pos = wall.transform.position;
            wall.transform.position = new Vector3(pos.x, pos.y, pos.z+1);
        }
        foreach (var obj in generatedObjects)
        {
            Destroy(obj);
        }
        GenerateWalls();
        GenerateFloor();
    }
    public static void AddNewFurniture(string item, GameObject furniture)
    {
        var position = furniture.transform.position;
        var rotation = furniture.transform.rotation;
        CoordinatesSaver obj = new CoordinatesSaver(item, position.x, position.y, position.z, rotation.x, rotation.y,
            rotation.z, rotation.w);
        _furnitures.Add(obj);
    }
    public static void DeleteFurniture(string item, GameObject furniture)
    {
        var position = furniture.transform.position;
        var rotation = furniture.transform.rotation;
        CoordinatesSaver obj1 = new CoordinatesSaver(item, position.x, position.y, position.z, rotation.x, rotation.y,
            rotation.z, rotation.w);
        foreach (var fur in _furnitures)
        {
            if (obj1 == fur)
            {
                _furnitures.Remove(fur);
                Debug.Log(fur + "deleted from list");
            }
        }
        CoordinatesSaver obj = new CoordinatesSaver(item, position.x, position.y, position.z, rotation.x, rotation.y,
            rotation.z, rotation.w);
        _furnitures.Remove(obj);
    }
    private void InstantiateFurniture()
    {
        foreach (var furniture in _furnitures)
        {
            GameObject obg = (GameObject) Instantiate(_furnitureSo.GetIngredient(furniture.type).Prefab);
            obg.transform.position = new Vector3(furniture.pos_x, furniture.pos_y, furniture.pos_z);
            obg.transform.rotation = new Quaternion(furniture.rot_x, furniture.rot_y, furniture.rot_z, furniture.rot_w);
        }
    }

    public void Save() {

        FurnitureModel data = new FurnitureModel() {
            roomSize_x = x,
            roomSize_y = y,
            CoordinatesFurniture = _furnitures
        };

        SaveAndLoad.SaveAndLoad.Save(data,"/Room", "RoomData");
    }
    private void Load(FurnitureModel data) {
        if (data != null)
        {
            _furnitures = data.CoordinatesFurniture;
        }
        InstantiateFurniture();
    }
    public void Load()
    {
        FurnitureModel data = (FurnitureModel) SaveAndLoad.SaveAndLoad.Load("/Room", "RoomData",
            ModelTypesEnums.FurnitureModel);
        if (data != null)
        {
            _furnitures = data.CoordinatesFurniture;
        }
        InstantiateFurniture();
    }

    public static bool FurnitureExist(string furniture)
    {
        foreach (var obj in _furnitures)
        {
            var tmp = "barTable" + obj.type;
            if (tmp == furniture)
            {
                return true;
            }
        }
        return false;
        
    }

    public int FurnitureCounter()
    {
        int counter = 0;
        foreach (var obj in _furnitures)
        {
            if (obj.type == "Table")
            {
                counter++;
            }
        }

        return counter;
    }
}
