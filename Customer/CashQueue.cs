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
    [SerializeField] private Animator _animator;


    public bool canMove;
    public OrderController oc;
    public bool orderNew;
    public bool orderFinished;


    public bool tryToCreateOrder = true;

    int NextPosIndex;
    Vector3 NextPos;

    public int posi;
    

    void Start()
    {

        if (gc.pos2Free && gc.pos1Free && gc.pos0Free)
        {
            posi = 0;
        }

        if (gc.pos0Free == false && gc.pos1Free == true)
        {
            posi = 1;
        }

        if(gc.pos0Free == false && gc.pos1Free == false && gc.pos2Free)
        {
            posi = 2;
        }

    }


    void Update()
    {

        gc.UpdatePos();
        NextPos = gc.GetFreePos(posi);
        
        if (transform.position.x == NextPos.x)
            _animator.SetBool("Running", false);
        else
            _animator.SetBool("Running", true);
        
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
                Destroy(this.gameObject);
                //gc.posExit = true;
                sc.currentAmountOfCustomers--;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos, ObjectSpeed * Time.deltaTime);
        }

        
        //moving to exit
        if (NextPos == gc.QueuePositions[6].transform.position)
        {
            transform.rotation = Quaternion.Euler(0, 180 , 0);
        }
        else
        {
            if (NextPos != gc.QueuePositions[0].transform.position)
            {
                this.transform.rotation = Quaternion.Euler(0, 90 , 0);
            }
        }

    }


    public void CreateOrder()
    {
        oc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<OrderManager>().CreateOrder();
        oc.OnStatusChange += OrderStatus;

    }

    void OrderStatus(Status status)
    {
        if (status == Status.Finished)
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
