using System.Collections;
using System.Collections.Generic;
using System.IO;
using Models;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private string saveFile = "Assets/Resourses/SavedData/Storage/StorageData";
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
        CafeManager.Paused = false;
        SceneManager.LoadScene(1);
    }
    public void onNew()
    {
        File.Delete(saveFile + "StorageData");
        CafeManager.Paused = false;
        SceneManager.LoadScene(1);
    }

    public void onContinue()
    {
        CafeManager.pauseGame();
        this.gameObject.SetActive(false);
    }
}
