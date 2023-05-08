using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customerMovement : MonoBehaviour
{
    [SerializeField] float customerSpeed;

    private SpawnCustomer sc;
    string[] CoffeeList = { "Hot Coffee", "Cold Coffee", "Latte" };
    string[] BakeryListItem = { "Doughnut", "Muffin" };

    private Vector3 CashRegister;
    private bool MoveToCashRegister;
    private Vector3 FreeTable;
    private bool MoveToFreeTable;
    private Vector3 Exit;
    private bool MoveToExit;

    public Text coffeeInfo;
    public Text bakeryInfo;
    int orderNumber;
    private bool allowOrder;

    private void Start()
    {
        orderNumber = 0;
        bakeryInfo.enabled = false;
        coffeeInfo.enabled = false;
        allowOrder = false;
        MoveToCashRegister = true;
    }

    void Update()
    {
        MoveCustomer();

    }

    void MoveCustomer()
    {
        CashRegister = new Vector3(315f, 1.52f, 165.79f);
        FreeTable = new Vector3(307f, 1.52f, 164.53f);
        Exit = new Vector3(300f, 1.52f, 165.49f);

        if (transform.position.x == Exit.x && transform.position.z == Exit.z)
        {
            Destroy(gameObject);
            bakeryInfo.enabled = false;
            coffeeInfo.enabled = false;
            
        }

            if (transform.position.x == CashRegister.x && transform.position.z == CashRegister.z)
        {

            if (allowOrder == false)
            {
              
                generateOrder();
                allowOrder = true;
                Instantiate(coffeeInfo);
                Instantiate(bakeryInfo);

            }
            

            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveToCashRegister = false;
                Debug.Log("Order accepted");
                MoveToFreeTable = true;
                
                transform.position = Vector3.MoveTowards(transform.position, FreeTable, customerSpeed * Time.deltaTime);
            }
        }

        else
        {

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveToFreeTable= false;
                Debug.Log("Order delivered");
                MoveToExit = true;
                transform.position = Vector3.MoveTowards(transform.position, Exit, customerSpeed * Time.deltaTime);
                
                Debug.Log("x1: " + orderNumber);
            }

            if (MoveToCashRegister == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, CashRegister, customerSpeed * Time.deltaTime);
            }

            if (MoveToFreeTable == true)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, FreeTable, customerSpeed * Time.deltaTime);
            }

            if (MoveToExit == true)
            {

                transform.position = Vector3.MoveTowards(transform.position, Exit, customerSpeed * Time.deltaTime);
            }

        }
    }

    public void generateOrder()
    {
        orderNumber++;  //todo is not working correctly, always shows 1
        Debug.Log(orderNumber);
        bakeryInfo.text = generateBakeryItem();
        bakeryInfo.enabled = true;
        coffeeInfo.text = generateCoffee();
        coffeeInfo.enabled = true;
        
        Debug.Log("Order " + orderNumber + " Info: " + bakeryInfo.text + ", " + coffeeInfo.text);
        

    }

    private string generateCoffee()
    {
        string chosenCoffee = CoffeeList[Random.Range(0, 3)];
        return chosenCoffee;
    }

    private string generateBakeryItem()
    {
        string chosenBakeryItem = BakeryListItem[Random.Range(0, 2)];
        return chosenBakeryItem;
    }
}
