using System.Collections;
using System.Collections.Generic;
using System.IO;
using Models;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {
    public static bool NewGame;

    public Button loadBtn;
    // Start is called before the first frame update
    void Start()
    {
        if (!SaveAndLoad.SaveAndLoad.IfLoadAvailable()) {
            loadBtn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onExit()
    {
        StorageController.Instance.Save();
        Application.Quit(0);
    }
    
    public void onNew() {
        NewGame = true;
        CafeManager.Paused = false;
        SceneManager.LoadScene(1);
    }
    public void onLoad() {
        NewGame = false;
        CafeManager.Paused = false;
        SceneManager.LoadScene(1);
    }
    public void onContinue()
    {
        CafeManager.pauseGame();
        this.gameObject.SetActive(false);
    }
}
