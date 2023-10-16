using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class employeeOrder : MonoBehaviour
{
    public customerMovement cm;
    public bool orderAccepted; 
    private Vector3 FreeTable;
    private Vector3 CashRegister;
    public GameObject employee;
    public bool timerPass;
    public bool moveBack;
    public bool canMove;
    public bool passOrder;
    [SerializeField] float employeeSpeed;

    private void Start()
    {
        moveBack = false;
        canMove = true;
        passOrder = false;
        FreeTable = new Vector3(307f, 1.52f ,167.5f);
        CashRegister = new Vector3(315f, 1.52f, 168.6f);

    }

    public void Update()
    {
       

        if (orderAccepted == true)
        {
            acceptOrder();
          //  Debug.Log("yyyyy");
        }

        if (moveBack == true)
        {
            canMove = false;
            transform.position = Vector3.MoveTowards(employee.transform.position, CashRegister, employeeSpeed * Time.deltaTime);
            StartCoroutine(timer());
            deliverOrder();
        }


    }

    public void acceptOrder()
    {
        if (canMove == true)
        {
            transform.position = Vector3.MoveTowards(employee.transform.position, FreeTable, employeeSpeed * Time.deltaTime);
            Debug.Log("moving to table");
            StartCoroutine(timer());
        }
        //moveBack = true;
        //if (moveBack == true)  
        //transform.position = Vector3.MoveTowards(employee.transform.position, CashRegister, employeeSpeed * Time.deltaTime);

    }

    public void deliverOrder()
    {
        StartCoroutine(timer1());
        if(passOrder==true)
        cm.all = true;
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
        moveBack = true;
        canMove = false;
       // Debug.Log("Preparing order");
        
    }

    public IEnumerator timer1()
    {
        yield return new WaitForSeconds(4.0f);
        passOrder = true;
        // Debug.Log("Preparing order");

    }


}
