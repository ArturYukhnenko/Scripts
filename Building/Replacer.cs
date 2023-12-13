using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replacer : MonoBehaviour
{
    [SerializeField]
    private GameObject buildSystem;
    [SerializeField]
    private string name;
    
    public void ReplaceObj()
    {
        GameObject ourBuildSystem = Instantiate(buildSystem);
        ourBuildSystem.gameObject.GetComponent<PlacementSystem>().StartPlacement(name);
        RoomGenerator.DeleteFurniture(name, this.gameObject);
        Destroy(this.gameObject);
    }
}
