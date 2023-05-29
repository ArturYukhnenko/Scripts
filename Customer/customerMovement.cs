using System.Collections;
using System.Collections.Generic;
using Ordering;
using Storage;
using UnityEngine;
using UnityEngine.UI;

public class customerMovement : MonoBehaviour
{
    [SerializeField] float customerSpeed;

    private SpawnCustomer sc;
    //string[] CoffeeList = { "Hot Coffee", "Cold Coffee", "Latte" };
    //string[] BakeryListItem = { "Doughnut", "Muffin" };

    private employeeOrder or;
    private GameObject order;

    private Vector3 CashRegister;
    private bool MoveToCashRegister;
    private Vector3 FreeTable;
    private bool MoveToFreeTable;
    private Vector3 Exit;
    public bool MoveToExit;
    public bool _moveToExit;

    public Text coffeeInfo;
    public Text bakeryInfo;
    int orderNumber;
    public bool allowOrder;
    public bool all;
    public bool x;

    private void Awake()
    {
        order = GameObject.Find("Employee");
        or = order.GetComponent<employeeOrder>();
        or.SetCustomer(this.gameObject);

    }

    private void Start()
    {
        //order =  GameObject.Find("Employee");
        //or = order.GetComponent<employeeOrder>();
        orderNumber = 0;
        bakeryInfo.enabled = false;
        coffeeInfo.enabled = false;
        allowOrder = false;
        MoveToCashRegister = true;
    }

    void Update()
    {

        MoveCustomer();

        if (allowOrder == true)
        {

            or.orderAccepted = true;
            //Debug.Log("xxxx");
        }

        Debug.Log(MoveToCashRegister + "movetocashregister");
        Debug.Log(MoveToFreeTable + "movetofreetable");
        Debug.Log(MoveToExit + "movetoexit");

    }

    void MoveCustomer()
    {
        CashRegister = new Vector3(315f, 1.52f, 165.79f);
        FreeTable = new Vector3(307f, 1.52f, 164.53f);
        Exit = new Vector3(300f, 1.52f, 165.49f);

        if (transform.position.x == Exit.x && transform.position.z == Exit.z)
        {
            DestroyImmediate(gameObject, true);
            //gameObject.SetActive(false);
            Destroy(bakeryInfo);
            Destroy(coffeeInfo);
            all = false;
            x = true;
            bakeryInfo.enabled = false;
            coffeeInfo.enabled = false;
            //MoveToExit = false;
            //MoveToCashRegister = true;
        }

            if (transform.position.x == CashRegister.x && transform.position.z == CashRegister.z)
        {

            if (allowOrder == false)
            {
                GameObject.Find("OrderManager").GetComponent<OrderManager>().CreateOrder(StorageController.Instance.ReceiveActualDishes());
                Debug.Log(StorageController.Instance.ReceiveActualDishes().Count);
                //generateOrder();dsa
                allowOrder = true;
                Instantiate(coffeeInfo);
                Instantiate(bakeryInfo);


            }
            

            if (all==true)
            {
                MoveToCashRegister = false;
                
                MoveToFreeTable = true;
       
                transform.position = Vector3.MoveTowards(transform.position, FreeTable, customerSpeed * Time.deltaTime);
            }
        }

        else
        {

            if (Input.GetKeyDown(KeyCode.D) || _moveToExit == true)
            {
                MoveToFreeTable= false;
                customerSpeed = 2.0f;
                MoveToExit = false;
                transform.position = Vector3.MoveTowards(transform.position, Exit, customerSpeed * Time.deltaTime);
                
                
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

    //public void generateOrder()
    //{
    //    orderNumber++;  //todo is not working correctly, always shows 1
    //    //Debug.Log(orderNumber);
    //    bakeryInfo.text = generateBakeryItem();
    //    bakeryInfo.enabled = true;
    //    coffeeInfo.text = generateCoffee();
    //    coffeeInfo.enabled = true;
        
    //    //Debug.Log("Order " + orderNumber + " Info: " + bakeryInfo.text + ", " + coffeeInfo.text);
        

    //}

    //private string generateCoffee()
    //{
    //    //string chosenCoffee = CoffeeList[Random.Range(0, 3)];
    //    return chosenCoffee;
    //}

    //private string generateBakeryItem()
    //{
    //    //string chosenBakeryItem = BakeryListItem[Random.Range(0, 2)];
    //    return chosenBakeryItem;
    //}

}
