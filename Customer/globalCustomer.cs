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
    //[SerializeField] public Transform[] TablePositions;

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

    public bool tryToCreateOrder = true;
    public OrderController oc;

    public bool moveToNextTable;
    public bool moveToExit;

    void Start()
    {
        //moveToNextTable = true;

        pos2Free = true;
        QueuePositionsOccupation();

        //posExit = true;
        //posTableExit = true;
        posTable3 = true;
        posTable2 = true;
        posTable1 = true;

        UpdatePos();

    }

    public Vector3 GetFreePos(int index)
    {
        return QueuePositions[index].position;
    }

    //public Vector3 GetTableFreePos(int index)
    //{
    //    return TablePositions[index].position;
    //}

    public int AddCust(CashQueue cust)
    {
        custList.Add(cust);
        return custList.Count - 1;
    }


    void Update()
    {
        //Debug.Log("size: " + guestList.Count);
        //Debug.Log("table1: " + posTable1);
        //Debug.Log("table2: " + posTable2);
        //Debug.Log("table3: " + posTable3);
        //Debug.Log("move: " + moveToNextTable);
        //Debug.Log("move: " + QueuePositions.Length);
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
                    oc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OrderManager>().CreateOrder(StorageController.Instance.ReceiveActualDishes());
                    oc.OnStatusChange += LeaveCafe;
                    tryToCreateOrder = false;
                }
                moveToNextTable = true;
                pos0Free = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().posi == QueuePositions[1].transform.position.x)
            {
                pos1Free = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().posi == QueuePositions[2].transform.position.x)
            {
                pos2Free = false;
            }

            //if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[3].transform.position.x)
            //{
            //    posTable1 = false;
            //}

        }
    }

    public void FreeTablesOccupation()
    {
        for (int i = 0; i < guestList.Count; i++)
        {
            GameObject temp = guestList[i].gameObject;

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[3].transform.position.x)
            {
                posTable1 = false;
                moveToExit = true;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[4].transform.position.x)
            {
                moveToExit = true;
                posTable2 = false;
                pos0Free = true;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[5].transform.position.x)
            {
                posTable3 = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == QueuePositions[6].transform.position.x)
            {
                //guestList[i].gameObject.SetActive(false);
                foreach (var x in guestList)
                {
                    Debug.Log("x1 "+x);
                }



                guestList.Remove(guestList[i]);
                this.guestList[i].gameObject.SetActive(false);
                //Destroy(guestList[i]);
                foreach (var x in guestList)
                {
                    Debug.Log("x2 " + x);
                }

                //Debug.Log("x2"+guestList);
                //guestList.Remove(this.guestList[0].gameObject);
                
                
                //posExit = false;
            }

        }
    }


    public void UpdatePos()
    {

        for (int i = 0; i < guestList.Count; i++)
        {
            //Debug.Log(guestList[i].transform.position.x == QueuePositions[1].transform.position.x);

            if (guestList[i].gameObject.transform.position.x == QueuePositions[1].transform.position.x && pos0Free && posTable1 == false)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 0;
                pos1Free = true;

            }

            if (guestList[i].gameObject.transform.position.x == QueuePositions[2].transform.position.x && pos1Free && pos0Free == false)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 1;
                pos2Free = true;
            }
            if (Input.GetKeyDown("n") && guestList[i].gameObject.transform.position.x == QueuePositions[0].transform.position.x)
            {
   

                if (posTable1 && moveToNextTable)
                {
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 3;
                    pos0Free = true;
                    moveToNextTable = false;
                    //this.gameObject.GetComponent<globalCustomer>().moveToNextTable = false;
                    //break;
                }

                if (posTable2 && moveToNextTable)
                {
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 4;
                    //posTable2 = false;
                    //if(posTable2 == false)
                    //pos0Free = true;
                    moveToNextTable = false;
                   //break;
                }
                if (posTable3 && moveToNextTable)
                {
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 5;
                    //pos0Free = true;
                    moveToNextTable = false;
                }


                //guestList[i].gameObject.GetComponent<CashQueue>().posi = 3;
                //pos0Free = true;
            }

            if (Input.GetKeyDown("d") && posTable1 == false)
            {
                if(guestList[i].gameObject.GetComponent<CashQueue>().posi == 3)
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
            }

            if (Input.GetKeyDown("f") && posTable2 == false)
            {
                if (guestList[i].gameObject.GetComponent<CashQueue>().posi == 4)
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
            }

            if (Input.GetKeyDown("g") && posTable2 == false)
            {
                if (guestList[i].gameObject.GetComponent<CashQueue>().posi == 5)
                    guestList[i].gameObject.GetComponent<CashQueue>().posi = 6;
            }
        }
    }



    void LeaveCafe(Status status)
    {
        if (status == Status.Finished)
        {
            
            oc.OnStatusChange -= LeaveCafe;
            pos1Free = true;
        }
    }

}
