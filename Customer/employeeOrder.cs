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
    [SerializeField] float employeeSpeed;

    private void Start()
    {
        FreeTable = new Vector3(307f, 1.52f, 167.5f);
        FreeTable = new Vector3(315f, 1.52f, 168.6f);

    }

    public void Update()
    {
       

        if (orderAccepted == true)
        {
            acceptOrder();
      
        }

        
        
    }

    public void acceptOrder()
    {
        transform.position = Vector3.MoveTowards(employee.transform.position, FreeTable, employeeSpeed * Time.deltaTime);
        Debug.Log("moving to table");
        StartCoroutine(timer());
        //transform.position = Vector3.MoveTowards(employee.transform.position, CashRegister, employeeSpeed * Time.deltaTime);

    }

    public void deliverOrder()
    {

        cm.all = true;
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Preparing order");
        
    }

}
