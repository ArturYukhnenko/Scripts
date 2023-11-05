using System.Collections;
using System.Collections.Generic;
using Storage;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseStorePopup : PopupBehavior
{
    [SerializeField]
    private GameObject onlineShoppingPopup, furnitureShoppingPopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoFair()
    {
        SceneManager.LoadScene(1);
    }

    public void OnlineShopping()
    {
        var popupObj = Instantiate(onlineShoppingPopup);
        base.DestroyPopup();
    }

    public void FurnitureShopping()
    {
        var popupObj = Instantiate(furnitureShoppingPopup);
        base.DestroyPopup();
    }
}
