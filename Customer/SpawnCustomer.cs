using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public GameObject customerPrefab;
    private int currentAmountOfCustomers;

    private void Update()
    {
        if (currentAmountOfCustomers <3 && Input.GetKeyDown(KeyCode.Space))
        {
            
            customerPrefab.SetActive(true);
            Instantiate(customerPrefab, transform.position, Quaternion.identity);
            currentAmountOfCustomers++;
            customerPrefab.transform.Rotate(0f, 180f, 0f);
           
        }
    }
}
