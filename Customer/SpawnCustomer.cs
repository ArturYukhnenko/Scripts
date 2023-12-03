using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomer : MonoBehaviour
{
    public globalCustomer gc;
    public SpawnCustomer sc;
    public CashQueue cq;
    public GameObject customerPrefab;
    public int currentAmountOfCustomers;


    private float minSpawnTime = 10f;
    private float maxSpawnTime = 15f;

    private float timer;
    private bool shouldCreateObject = false;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !shouldCreateObject && currentAmountOfCustomers<3)
        {
            currentAmountOfCustomers++;
            shouldCreateObject = true;
            CreateObject();
            ResetTimer();
        }

        if(currentAmountOfCustomers == 3)
        {
            ResetTimer();
        }
    }

    void CreateObject()
    {
        GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
        customer.GetComponent<customerMovement>().customer = customer;
        customer.GetComponent<CashQueue>().gc = gc;
        customer.GetComponent<CashQueue>().sc = sc;
    }

    void ResetTimer()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
        shouldCreateObject = false;
    }

}


