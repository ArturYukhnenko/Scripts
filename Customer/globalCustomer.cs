using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class globalCustomer : MonoBehaviour
{

    [SerializeField] public Transform[] Positions;


    [SerializeField] CashQueue cq;
    [SerializeField] SpawnCustomer sc;
    public List<GameObject> guestList = new List<GameObject>();
    public List<CashQueue> custList = new List<CashQueue>();
    GameObject[] gs;

    public int pos;

    public int amountOfCustomers;

    public bool posExit;
    public bool posTableExit;
    public bool posTable;
    public bool pos2Free;
    public bool pos1Free;
    public bool pos0Free;



    void Start()
    {
        pos2Free = true;
        PositionsOccupation();

        posExit = true;
        posTableExit = true;
        posTable = true;

        UpdatePos();

    }

    public Vector3 GetFreePos(int index)
    {
        return Positions[index].position;
    }

    public int AddCust(CashQueue cust)
    {
        custList.Add(cust);
        return custList.Count - 1;
    }


    void Update()
    {

        PositionsOccupation();
        //Debug.Log("pos0free: " + pos0Free);
        //Debug.Log("pos1free: " + pos1Free);
        //Debug.Log("pos2free: " + pos2Free);
        //Debug.Log("amount: " + guestList.Count);
        amountOfCustomers = custList.Count;

        gs = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject g in gs)
        {
            if (!guestList.Contains(g.gameObject))
                guestList.Add(g.gameObject);
            pos = guestList.IndexOf(g.gameObject);
        }

    }

    public void PositionsOccupation()
    {
        for (int i = 0; i < guestList.Count; i++)
        {

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == cq.Positions[0].transform.position.x)
            {
                pos0Free = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().posi == cq.Positions[1].transform.position.x)
            {
                pos1Free = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().posi == cq.Positions[2].transform.position.x)
            {
                pos2Free = false;
            }

            if (guestList[i].gameObject.GetComponent<CashQueue>().transform.position.x == cq.Positions[3].transform.position.x)
            {
                posTable = false;
            }

        }
    }


    public void UpdatePos()
    {

        for (int i = 0; i < guestList.Count; i++)
        {
            Debug.Log(guestList[i].transform.position.x == cq.Positions[1].transform.position.x);
            Debug.Log("pos0Freexxx: " + pos0Free);
            Debug.Log("postablefreexxx:" + posTable);

            if (guestList[i].gameObject.transform.position.x == cq.Positions[1].transform.position.x && pos0Free && posTable == false)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 0;
                pos1Free = true;

            }

            if (guestList[i].gameObject.transform.position.x == cq.Positions[2].transform.position.x && pos1Free && pos0Free == false)
            {
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 1;
                pos2Free = true;
            }

            if (Input.GetKeyDown("n") && guestList[i].gameObject.transform.position.x == cq.Positions[0].transform.position.x)
            {

                //Debug.Log("lol wow a");
                guestList[i].gameObject.GetComponent<CashQueue>().posi = 3;
                //posTable = false;
                pos0Free = true;
            }

            if (Input.GetKeyDown("d") && guestList[i].gameObject.GetComponent<CashQueue>().posi == 3)
            {
                //sc.currentAmountOfCustomers--;
                Destroy(guestList[i].gameObject);
                guestList.Remove(guestList[i].gameObject);
                posTable = true;
            }
        }
    }

}
