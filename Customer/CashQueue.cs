using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CashQueue : MonoBehaviour
{


    [SerializeField] public globalCustomer gc;
    [SerializeField] public List<Transform> Positions = new List<Transform>();
    [SerializeField] float ObjectSpeed;



    public bool pos3Free;
    public bool pos2Free;
    public bool pos1Free;


    int NextPosIndex;
    Vector3 NextPos;

    public int posi;


    void Start()
    {
        //posi = gc.pos;

        posi = gc.AddCust(this);
        //NextPos = Positions[0];
        NextPos = gc.GetFreePos(posi);
        //NextPos = Positions[gc.pos];
        //NextPos = gc.Positions[gc.pos];


        pos3Free = false;
        pos2Free = false;
        pos1Free = false;


       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("xexexe: " + gc.pos);
        //Debug.Log("eeee: " + gc.guestList.IndexOf(gameObject));
        //Debug.Log("position1: "+pos1Free);
        //Debug.Log("position2: " + pos2Free);
        //Debug.Log("posindex:  " + NextPosIndex);
        //Debug.Log("posindexposition:  " + NextPos.position.x);
        //Debug.Log("nextposindex: " + NextPosIndex);
        //Debug.Log("nextpos" + NextPos);
        //Debug.Log("posit: " + NextPos.position.x);
        MoveGameObject();
        


        //Debug.Log("djasjdhksa: " + NextPos.position.x);
    }


    public void MoveGameObject()//Transform transform)
    {
        //this.NextPosIndex = nextPosindex;



        //NextPos = Positions[NextPosIndex];
        //NextPos = Positions[gc.guestList.IndexOf(gameObject)];




        //if (pos1Free)
        //{
        //    NextPosIndex=2;
        //}

        //if (pos2Free)
        //{
        //    NextPosIndex = 1;
        //}

        //if (pos3Free)
        //{
        //    NextPosIndex = 0;
        //}


        //if (pos1Free == true)
        //    {
        //        //Debug.Log("moving1...");
        //        transform.position = Vector3.MoveTowards(transform.position, NextPos.position, ObjectSpeed * Time.deltaTime);
        //    }

        //if (pos2Free == true)
        //{
        //    //Debug.Log("moving2...");
        //    transform.position = Vector3.MoveTowards(transform.position, NextPos.position, ObjectSpeed * Time.deltaTime);
        //}

        //if (pos3Free == true)
        //{
        //    //Debug.Log("moving3...");
        //    transform.position = Vector3.MoveTowards(transform.position, NextPos.position, ObjectSpeed * Time.deltaTime);
        //}


        //if(transform.position.x == NextPos.position.x && transform.position.z == NextPos.position.z)
        //{
        //    pos1Free = false;
        //    pos2Free = true;
        //}




        if (transform.position.x == NextPos.x) //)
        {

            //if (Input.GetKeyDown(KeyCode.N))
                //NextPosIndex++;

            if (NextPosIndex >= Positions.Count)
            {
                NextPosIndex = 0;
            }

            if (transform.position.x == Positions[5].position.x)
                Destroy(gameObject);

            //NextPos = Positions[NextPosIndex];


        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos, ObjectSpeed * Time.deltaTime);
        }


    }

   
}
