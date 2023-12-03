using System;
using System.Collections;
using System.Collections.Generic;
using Ordering;
using Storage;
using UnityEngine;
using static MenuEquipment.SO.Menu;
using static UnityEngine.ParticleSystem;

public class CashQueue : MonoBehaviour
{


    [SerializeField] public globalCustomer gc;
    [SerializeField] float ObjectSpeed;
    [SerializeField] public SpawnCustomer sc;

    public bool canMove;
    public OrderController oc;


    public bool tryToCreateOrder = true;

    int NextPosIndex;
    Vector3 NextPos;

    public int posi;
    

    void Start()
    {

        //if (gc.pos0Free)
        //{
        //    posi = 0;

        //}
        //else
        //{
        //    posi = gc.AddCust(this);
        //}

        if (gc.pos0Free)
        {
            posi = 0;
        }

        if (gc.pos1Free)
        {
            posi = 1;
        }

        if (gc.pos2Free)
        {
            posi = 2;
        }


        //if (gc.custList.IndexOf(this) == 0)
        //{
        //    posi = 0;
        //}

        //if (gc.custList.IndexOf(this) == 1)
        //{
        //    posi = 1;
        //}

        //if (gc.custList.IndexOf(this) == 2)
        //{
        //    posi = 2;
        //}

    }


    void Update()
    {

        gc.UpdatePos();
        NextPos = gc.GetFreePos(posi);

        MoveGameObject();
    }


    public void MoveGameObject()
    {

        if (transform.position.x == NextPos.x)
        {

            if (NextPosIndex >= gc.QueuePositions.Length)
            {
                NextPosIndex = 0;
            }

            if (this.gameObject.GetComponent<CashQueue>().transform.position.x == gc.QueuePositions[6].transform.position.x)
            {
                gc.guestList.Remove(this.gameObject);
                //Debug.Log("Index of the obejct: " + gc.guestList.IndexOf(this.gameObject));
                //Debug.Log("Index of the obejct cust list: " + gc.custList.IndexOf(this));
                Destroy(this.gameObject);
                gc.posExit = true;
                //sc.currentAmountOfCustomers--;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos, ObjectSpeed * Time.deltaTime);
        }


    }

}
