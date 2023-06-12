using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class employeeOrder : MonoBehaviour
{
    //public customerMovement cm;
    public bool orderAccepted;
    private Vector3 FreeTable;
    private Vector3 CashRegister;
    public GameObject employee;
    public bool timerPass;
    public bool moveBack;
    public bool canMove;
    public bool passOrder;
    [SerializeField] float employeeSpeed;

    private customerMovement cm;
    private GameObject customer;


    //private void Awake()
    //{
    //    Debug.Log(GameObject.Find("Customer"));
    //    Debug.Log(customer.GetComponent<customerMovement>());
    //    customer = GameObject.Find("Customer");
    //    cm = customer.GetComponent<customerMovement>();

    //}

    private void Start()
    {

        //customer = GameObject.Find("Customer");
        //cm = customer.GetComponent<customerMovement>();

        moveBack = false;
        canMove = true;
        passOrder = false;
        FreeTable = new Vector3(307f, 1.52f, 167.5f);
        CashRegister = new Vector3(315f, 1.52f, 168.6f);

    }

    public void Update()
    {

        //if (transform.position.x == CashRegister.x && transform.position.z == CashRegister.z)
        //{
        //    canMove = true;
        //}


        if (orderAccepted == false)
            canMove = true;

            if (orderAccepted == true)
        {
            acceptOrder();
            orderAccepted = false;
            //Debug.Log("yyyyy");
        }

        if (moveBack == true)
        {
            //canMove = false;
            transform.position = Vector3.MoveTowards(employee.transform.position, CashRegister, employeeSpeed * Time.deltaTime);

            if (transform.position.x == CashRegister.x && transform.position.z == CashRegister.z)
            {
                if (cm.transform.position.x == cm.CashRegister.x && cm.transform.position.z == cm.CashRegister.z)
                passOrder = true;
            }
                StartCoroutine(timer());
            deliverOrder();
        }


    }

    public void acceptOrder()
    {
        if (canMove == true)
        {
            if (cm.MoveToCashRegister == true)
            {

                transform.position = Vector3.MoveTowards(employee.transform.position, FreeTable, employeeSpeed * Time.deltaTime);
                //Debug.Log("moving to table");
                StartCoroutine(timer());
            }
        }
        //moveBack = true;
        //if (moveBack == true)  
        //transform.position = Vector3.MoveTowards(employee.transform.position, CashRegister, employeeSpeed * Time.deltaTime);

    }

    public void deliverOrder()
    {
        //StartCoroutine(timer1());
        Debug.Log("pass order: "+passOrder);
        if (passOrder == true)
        {
            //cm.allowOrder = false;
            cm.MoveToFreeTable = true;
            cm.MoveToCashRegister = false;
            passOrder = false;
        }
        StartCoroutine(timer2());
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
        if (transform.position.x == FreeTable.x && transform.position.z == FreeTable.z)
        {
            moveBack = true;
            canMove = false;
        }
        // Debug.Log("Preparing order");

    }

    //public IEnumerator timer1()
    //{
    //    yield return new WaitForSeconds(4.0f);
    //    passOrder = true;
    //    // Debug.Log("Preparing order");

    //}

    public IEnumerator timer2()
    {
        yield return new WaitForSeconds(8.0f);


        if (cm.transform.position.x == cm.FreeTable.x && cm.transform.position.z == cm.FreeTable.z)
        {
            cm.MoveToFreeTable = false;
            cm.MoveToExit = true;
        }
        // Debug.Log("Preparing order");

    }

    public void SetCustomer(GameObject _customer)
        {

        moveBack = false;
        customer = _customer;
        cm = customer.GetComponent<customerMovement>();


    }


}
