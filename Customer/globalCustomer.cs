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
    public List<GameObject> guestList =  new List<GameObject>();
    public List<CashQueue> custList = new List<CashQueue>();
    GameObject[] gs;

    public int pos;

    public int amountOfCustomers;



    void Start()
    {
        

        //Debug.Log("free pos1: "+GetFreePos(1));
        //Debug.Log("free pos2: " + GetFreePos(2));
        //Debug.Log("free pos0: " + GetFreePos(0));


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
        //Debug.Log("pos0free: " + cq.pos0Free);
        //Debug.Log("pos1free: " + cq.pos1Free);
        //Debug.Log("pos2free: " + cq.pos2Free);
        //Debug.Log("postablefree: " + cq.posTable);
        //UpdatePos();
        amountOfCustomers = custList.Count;
       //Debug.Log("amount of cust in list: " + amountOfCustomers);
        //Debug.Log("xxx: " + guestList[0].gameObject.transform.position.x);

        gs = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject g in gs)
        {
            //Debug.Log("length: " + gs.Length);
            if(!guestList.Contains(g.gameObject))
            guestList.Add(g.gameObject);
            pos = guestList.IndexOf(g.gameObject);
            
            //Debug.Log("pos: " + pos);


        }


        //Debug.Log("cust1: " + guestList[0].transform.position.x);
        //Debug.Log("cust2: " + guestList[1].transform.position.x);
        //Debug.Log("cust3: " + guestList[2].transform.position.x);
    }



    public void AddGuest(GameObject guest)
    {
        
        //cq.MoveGameObject(cqPositions[guestList.IndexOf(guest)]);
    }


    public void UpdatePos()
    {
        //for (int i = 0; i < guestList.Count; i++)
        //{
        //    if (guestList[i].transform.position.x == Positions[0].transform.position.x)
        //    {
        //        cq.pos0Free = false;
               
        //    }
        //    if (guestList[i].transform.position.x == Positions[1].transform.position.x)
        //    {
        //        cq.pos1Free = false;
        //    }
        //    if (guestList[i].transform.position.x == Positions[2].transform.position.x)
        //    {
        //        cq.pos2Free = false;
                
        //    }
        //    if (guestList[i].transform.position.x == Positions[3].transform.position.x)
        //    {
        //        cq.posTable = false;
                
        //    }
        //    if (guestList[i].transform.position.x == Positions[4].transform.position.x)
        //    {
        //        cq.posTableExit = false;
                
        //    }
        //    if (guestList[i].transform.position.x == Positions[5].transform.position.x)
        //    {
        //        cq.posExit = false;
                
        //    }


            //if (guestList[i].transform.position.x != Positions[0].transform.position.x)
            //{
            //    cq.pos0Free = true;
            //}
            //if (guestList[i].transform.position.x != Positions[1].transform.position.x)
            //{
            //    cq.pos1Free = true;

            //}
            //if (guestList[i].transform.position.x != Positions[2].transform.position.x)
            //{
            //    cq.pos2Free = true;
            //}
            //if (guestList[i].transform.position.x != Positions[3].transform.position.x)
            //{
            //    cq.posTable = true;
            //}
            //if (guestList[i].transform.position.x != Positions[4].transform.position.x)
            //{
            //    cq.posTableExit = true;
            //}
            //if (guestList[i].transform.position.x != Positions[5].transform.position.x)
            //{
            //    cq.posExit = true;
            //}


            //if (Input.GetKeyDown("l") && cq.posi == 2 && gameObject.transform.position.x == Positions[2].position.x && cq.pos1Free)
            //{
            //    Debug.Log("xwxwxw");
            //    cq.posi = 1;
            //}

            //if (Input.GetKeyDown("k") && cq.posi == 1 && gameObject.transform.position.x == Positions[1].position.x && cq.pos0Free)
            //{
            //    cq.posi = 0;
            //}

            //if (Input.GetKeyDown("n") && cq.posi == 0 && gameObject.transform.position.x == Positions[0].position.x)
            //{
            //    Debug.Log("lol");
            //    cq.posi = 3;
            //}
        }

    }
