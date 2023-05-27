using System.Collections;
using System.Collections.Generic;
using System.IO;
using Models;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private string saveFile = @"Assets/Resourses/SavedData/Storage/StorageData";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onExit()
    {
        Application.Quit(0);
    }
    public void onLoad()
    {
        SaveAndLoad.SaveAndLoad.Load("Assets/Resourses/SavedData/Storage", "StorageData", ModelTypesEnums.StorageModel);
        SceneManager.LoadScene(0);
    }
    public void onNew()
    {
        SaveAndLoad.SaveAndLoad.LoadNew("Assets/Resourses/SavedData/Storage", "StorageData", ModelTypesEnums.StorageModel);
        SceneManager.LoadScene(0);
        Application.Quit(0);
    }
}
