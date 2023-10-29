using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public globalCustomer gc;
    public CashQueue cq;
    public GameObject customerPrefab;
    private int currentAmountOfCustomers;

    private void Update()
    {
        if (currentAmountOfCustomers < 3 && Input.GetKeyDown(KeyCode.Space))
        {

            //customerPrefab.SetActive(true);
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            currentAmountOfCustomers++;
            customerPrefab.transform.Rotate(0f, 180f, 0f);
            customer.GetComponent<customerMovement>().MoveToExit = false;
            
            customer.GetComponent<customerMovement>().MoveToCashRegister = true;
            customer.GetComponent<customerMovement>().MoveToFreeTable = false;
            
            customer.GetComponent<customerMovement>().customer = customer;
            customer.GetComponent<CashQueue>().gc = gc;
            //customer.GetComponent<CashQueue>().pos0Free = false;
            //customer.GetComponent<CashQueue>().pos1Free = false;
            //customer.GetComponent<globalCustomer>().UpdatePos();
        }
    }

    public void DestroyCustomer(GameObject customer)
    {
        customer.SetActive(false); 
    }
}
