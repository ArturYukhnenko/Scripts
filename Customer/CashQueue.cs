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
    [SerializeField] public List<Transform> Positions = new List<Transform>();
    [SerializeField] public List<Transform> FreeTablePositions = new List<Transform>();
    [SerializeField] float ObjectSpeed;
    public SpawnCustomer sc;

    public bool canMove;
    public OrderController oc;


    public bool tryToCreateOrder = true;

    int NextPosIndex;
    Vector3 NextPos;

    public int posi;


    void Start()
    {

        if (gc.pos0Free)
        {
            posi = 0;

        }
        else
        {
            posi = gc.AddCust(this);
        }

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

            if (NextPosIndex >= Positions.Count)
            {
                NextPosIndex = 0;
            }

            if (transform.position.x == Positions[5].position.x)
            {
                gc.guestList.Remove(this.gameObject);
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos, ObjectSpeed * Time.deltaTime);
        }


    }

}
