using System;
using System.Collections;
using System.Collections.Generic;
using Ordering;
using Storage;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class globalCustomer : MonoBehaviour
{

    [SerializeField] public Transform[] QueuePositions;
    [SerializeField] CashQueue cq;
    [SerializeField] SpawnCustomer sc;
    public List<GameObject> guestList = new List<GameObject>();
    public List<CashQueue> custList = new List<CashQueue>();
    GameObject[] gs;

    public int pos;

    public int amountOfCustomers;

    public bool posExit;
    public bool posTable3;
    public bool posTable2;
    public bool posTable1;
    public bool pos2Free;
    public bool pos1Free;
    public bool pos0Free;
    public bool orderNew;
    public bool orderFinished;

    public bool tryToCreateOrder = true;
    public OrderController oc;

    public bool moveToNextTable;
    public bool moveToExit;

    void Start()
    {
        posExit = true;
        pos0Free = true;
        pos1Free = true;
        pos2Free = true;
        QueuePositionsOccupation();

        posTable3 = true;
        posTable2 = true;
        posTable1 = true;

    }

    public Vector3 GetFreePos(int index)
    {
        return QueuePositions[index].position;
    }

    public int AddCust(CashQueue cust)
    {
        custList.Add(cust);
        return custList.Count - 1;
    }


    void Update()
    {
        Debug.Log("try: " + tryToCreateOrder);
        QueuePositionsOccupation();
        FreeTablesOccupation();
        amountOfCustomers = custList.Count;

        gs = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject g in gs)
        {

            if (!guestList.Contains(g.gameObject))
                guestList.Add(g.gameObject);

            pos = guestList.IndexOf(g.gameObject);
        }

    }

    public void QueuePositionsOccupation()
    {
        for (int i = 0; i < guestList.Count; i++)
        {

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[0].transform.position.x)
            {
                if (tryToCreateOrder)
                {
                    //guestList[i].gameObject.GetComponent<CashQueue>().
                    oc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OrderManager>().CreateOrder();
                    //guestList[i].gameObject.GetComponent<CashQueue>().
                    oc.OnStatusChange += OrderStatus;
                    tryToCreateOrder = false;
                }

                moveToNextTable = true;
                pos0Free = false;
            }
           

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[1].transform.position.x)
            {
                pos1Free = false;
            }
            

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[2].transform.position.x)
            {
                pos2Free = false;
            }


        }
    }

    public void FreeTablesOccupation()
    {
        for (int i = 0; i < guestList.Count; i++)
        {

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[3].transform.position.x)
            {
                posTable1 = false;
                moveToExit = true;

            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[4].transform.position.x)
            {
                moveToExit = true;
                posTable2 = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[5].transform.position.x)
            {
                moveToExit = true;
                posTable3 = false;
            }

        }
    }


    public void UpdatePos()
    {

        for (int i = 0; i < guestList.Count; i++)
        {

            if (guestList[i].gameObject.transform.position.x == QueuePositions[1].transform.position.x && pos0Free)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 0;
                pos1Free = true;

            }

            if (guestList[i].gameObject.transform.position.x == QueuePositions[2].transform.position.x && pos1Free && pos0Free == false)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 1;
                pos2Free = true;
            }

            if (orderNew && guestList[i].gameObject.transform.position.x == QueuePositions[0].transform.position.x)
            {

                if (posTable1 && moveToNextTable)
                {
                    orderNew = false;
                    tryToCreateOrder = true;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 3;
                    pos0Free = true;
                    moveToNextTable = false;
                }

                if (posTable2 && moveToNextTable)
                {
                    orderNew = false;
                    tryToCreateOrder = true;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 4;
                    pos0Free = true;
                    moveToNextTable = false;
                }
                if (posTable3 && moveToNextTable)
                {
                    orderNew = false;
                    tryToCreateOrder = true;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 5;
                    pos0Free = true;
                    moveToNextTable = false;
                }

            }

            if (orderFinished) //&& posExit == true)
            {
                if(guestList[i].gameObject.GetComponent<CashQueue>().posi == 3)
                {
                    orderFinished = false;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
                    posExit = false;
                    posTable1 = true;
                }

                if (guestList[i].gameObject.GetComponent<CashQueue>().posi == 4)
                {
                    orderFinished = false;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
                    posExit = false;
                    posTable2 = true;
                }


                if (guestList[i].gameObject.GetComponent<CashQueue>().posi == 5)
                {
                    orderFinished = false;
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
                    posExit = false;
                    posTable3 = true;
                }

            }
        }
    }

    void OrderStatus(Status status)
    {
        if(status == Status.Finished)
        {
            orderFinished = true;
            oc.OnStatusChange -= OrderStatus;
        }

        if (status == Status.New)
        {
            orderNew = true;
            
        }

    }


}
