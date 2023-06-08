using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CafeManager : MonoBehaviour
{
    [SerializeField]private GameObject dayUI, nightUI, dayLightSpot, nightLightSpot, pausePopup;

    public static bool IsDay, Paused;
    // Start is called before the first frame update
    void Start()
    {
        IsDay = false;
        turnOnNightMode();
        Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopup.gameObject.SetActive(!Paused);
            pauseGame();
        }
        
    }

    public void turnOnDayMode()
    {
        IsDay = true;
        nightUI.gameObject.SetActive(false);
        dayUI.gameObject.SetActive(true);
        dayLightSpot.gameObject.SetActive(true);
        nightLightSpot.gameObject.SetActive(false);
        Debug.Log("Day Mode on");
    }
    public void turnOnNightMode()
    {
        IsDay = false;
        nightUI.gameObject.SetActive(true);
        dayUI.gameObject.SetActive(false);
        dayLightSpot.gameObject.SetActive(false);
        nightLightSpot.gameObject.SetActive(true);
        Debug.Log("Night Mode on");
    }

    public static void pauseGame()
    {
        
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
    }

}
