using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    private int x = 18;

    private int y = 10;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject floorPrefab;
    private List<GameObject> generatedObjects = new List<GameObject>();
    [SerializeField]private List<GameObject> xWallsAdding = new List<GameObject>();
    [SerializeField]private List<GameObject> yWallsAdding = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        generateWalls();
        generateFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateWalls()
    {
        //front side
        var frontFirstWall = GameObject.Find("WallPart (1)").transform;
        for (int i = 1; i < x-1; i++)
        {
            var wall = Instantiate(wallPrefab);
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 frontWallVector3 = new Vector3(frontFirstWall.transform.position.x + i, frontFirstWall.position.y,
                frontFirstWall.position.z);
            wall.transform.SetPositionAndRotation(frontWallVector3, frontFirstWall.rotation);
            generatedObjects.Add(wall);
        }
        //back side
        var backFirstWall = GameObject.Find("WallPart (6)").transform;
        for (int i = 1; i < x-1; i++)
        {
            var wall = Instantiate(wallPrefab);
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
            wall.transform.SetParent(GameObject.Find("walls").transform);
            Vector3 rightWallVector3 = new Vector3(rightFirstWall.transform.position.x , rightFirstWall.position.y ,
                rightFirstWall.position.z+ i);
            wall.transform.SetPositionAndRotation(rightWallVector3, rightFirstWall.rotation);
            generatedObjects.Add(wall);
        }
        
    }

    private void generateFloor()
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

    public void addArea()
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
        generateWalls();
        generateFloor();
    }


}
