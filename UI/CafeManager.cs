using System;
using MenuEquipment.SO;
using Ordering;
using Storage;
using TMPro;
using UnityEngine;

public class CafeManager : MonoBehaviour
{
    [SerializeField]private GameObject dayUI, nightUI, dayLightSpot, nightLightSpot, pausePopup;
    public static GameObject _pausePopup;
    [SerializeField]
    private RoomGenerator _roomGenerator;
    
    [SerializeField]
    private Menu menu;
    private static Menu _menu;
    public static bool IsDay, Paused;
    //prestige
    public static float prestigeCoefficient;
    [SerializeField] private float dishPrestige, furniturePrestige;

    public float DishPrestige {
        get => dishPrestige;
        set => dishPrestige = value;
    }

    // Start is called before the first frame update
    
    [SerializeField] private TMP_Text money;
    
    void Start()
    {
        try
        {
            //SerializeField
            //_roomGenerator.Load();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }

        IsDay = false;
        turnOnNightMode();
        //IsDay = true;
        //turnOnDayMode();
        Paused = false;
        _menu = menu;
        _pausePopup = pausePopup;
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          //  pausePopup.gameObject.SetActive(!Paused);
            pauseGame();
        }
        money.text = StorageController.Instance.GetAmountOfMoney().ToString();
    }

    public void turnOnDayMode()
    {
        prestigeCoefficient = CountPrestige();
        IsDay = true;
        nightUI.gameObject.SetActive(false);
        dayUI.gameObject.SetActive(true);
        dayLightSpot.gameObject.SetActive(true);
        nightLightSpot.gameObject.SetActive(false);
        SaveGame();
        Debug.Log("Day Mode on");
    }
    public void turnOnNightMode()
    {
        IsDay = false;
        nightUI.gameObject.SetActive(true);
        dayUI.gameObject.SetActive(false);
        dayLightSpot.gameObject.SetActive(false);
        nightLightSpot.gameObject.SetActive(true);
        try {
            this.gameObject.GetComponent<OrderManager>().CloseAllOrder();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
        try {
            SaveGame();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
        Debug.Log("Night Mode on");
    }

    public static void pauseGame()
    {
        
        Paused = !Paused;
        Time.timeScale = Paused ? 0f : 1f;
        _pausePopup.gameObject.SetActive(true);
    }

    private void SaveGame() {
        StorageController.Instance.Save();
        
        _roomGenerator.Save();
    }

    public void LoadGame() {
        if (!MainMenuController.NewGame) {
            StorageController.Instance.Load();
            _roomGenerator.Load();
        }
    }
    public static void CleanActivatedTogglesOnDelete()
    {
        if (_menu == null)
        {
            throw new NullReferenceException("SerializeField menu may be lost");
        }
        foreach (var dish in _menu.dishes)
        {
            if (!RoomGenerator.FurnitureExist(dish.Instrument()))
            {
                dish.activated = false;
            }
        }
    }

    private float CountPrestige()
    {
        
        float roomCoefficient = _roomGenerator.FurnitureCounter() * furniturePrestige;
        float dishCoefficient = _menu.ActiveDishCounter() * dishPrestige;
        Debug.Log($"roomCoef{roomCoefficient}, dishCoef {dishCoefficient}, furnitureCount {_roomGenerator.FurnitureCounter()}, dishCounter {_menu.ActiveDishCounter()}");
        return 1 + roomCoefficient + dishCoefficient;
    }
}
