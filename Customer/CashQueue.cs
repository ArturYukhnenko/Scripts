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
    public SpawnCustomer sc;


    public bool posExit;
    public bool posTableExit;
    public bool posTable;
    public bool pos2Free;
    public bool pos1Free;
    public bool pos0Free;
    public bool canMove;


    int NextPosIndex;
    Vector3 NextPos;

    public int posi;


    void Start()
    {
        posExit = true;
        posTableExit = true;
        posTable = true;
        pos2Free = true;
        pos1Free = true;
        pos0Free = true;

        posi = gc.AddCust(this);
        NextPos = gc.GetFreePos(posi);

       


       
    }


    void Update()
    {
        
        Debug.Log("pos0free: " + pos0Free);
        Debug.Log("pos1free: " + pos1Free);
        Debug.Log("pos2free: " + pos2Free);
        Debug.Log("postableFree: " + posTable);
        Debug.Log("postableExit: " + posTableExit);
        Debug.Log("posExit: " + posExit);



        NextPos = gc.GetFreePos(posi);


        MoveGameObject();
        //gc.UpdatePos();
        UpdatePosi();
        
    }


    public void MoveGameObject()
    {
        
        if (transform.position.x == NextPos.x) //)
        {

            if (NextPosIndex >= Positions.Count)
            {
                NextPosIndex = 0;
            }

            if (transform.position.x == Positions[5].position.x)
                gameObject.SetActive(false);
                //sc.DestroyCustomer(this.gameObject);

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos, ObjectSpeed * Time.deltaTime);
        }


    }

    public void UpdatePosi()
    {
        //for (int i = 0; i < gc.gc.guestList.Count; i++)
        //{
        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[0].transform.position.x)
        //    {
        //        pos0Free = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[1].transform.position.x)
        //    {
        //        pos1Free = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[2].transform.position.x)
        //    {
        //        pos2Free = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[3].transform.position.x)
        //    {
        //        posTable = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[4].transform.position.x)
        //    {
        //        posTableExit = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x == gc.Positions[5].transform.position.x)
        //    {
        //        posExit = false;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[0].transform.position.x)
        //    {
        //        pos0Free = true;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[1].transform.position.x)
        //    {
        //        pos1Free = true;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[2].transform.position.x)
        //    {
        //        pos2Free = true;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[3].transform.position.x)
        //    {
        //        posTable = true;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[4].transform.position.x)
        //    {
        //        posTableExit = true;
        //    }

        //    if (gc.gc.guestList[i].transform.position.x != gc.Positions[5].transform.position.x)
        //    {
        //        posExit = true;
        //    }
        //}

        for (int i = 0; i < gc.guestList.Count; i++)
        {
            if (gc.guestList[i].transform.position.x == Positions[0].transform.position.x)
            {
                pos0Free = false;
                pos1Free = true;
                
            }
            if (gc.guestList[i].transform.position.x == Positions[1].transform.position.x)
            {
                pos1Free = false;
            }
            if (gc.guestList[i].transform.position.x == Positions[2].transform.position.x)
            {
                pos2Free = false;

            }
            if (gc.guestList[i].transform.position.x == Positions[3].transform.position.x)
            {
                posTable = false;
                pos0Free = true;

            }
            if (gc.guestList[i].transform.position.x == Positions[4].transform.position.x)
            {
                posTableExit = false;
                posTable = true;

            }
            if (gc.guestList[i].transform.position.x == Positions[5].transform.position.x)
            {
                //sc.DestroyCustomer(this.gameObject);
                //gameObject.SetActive(false);
                //posExit = false;

            }
            
        }

        if (Input.GetKeyDown("l") && posi == 2 && gameObject.transform.position.x == gc.Positions[2].position.x && pos1Free)
        {
                pos2Free = true;
                posi = 1;
                
        }

        if (Input.GetKeyDown("k") && posi == 1 && gameObject.transform.position.x == gc.Positions[1].position.x && pos0Free)
        {
                pos1Free = true;
                posi = 0;
                
        }

        if (Input.GetKeyDown("n") && posi == 0 && gameObject.transform.position.x == gc.Positions[0].position.x && posTable)
        {
            pos0Free = true;
            posi = 3;
            pos0Free = true;
        }

        if (Input.GetKeyDown("n") && posi == 3 && gameObject.transform.position.x == gc.Positions[3].position.x && posTableExit)
        {
            posTable = true;
            posi = 4;
        }

        if (Input.GetKeyDown("n") && posi == 4 && gameObject.transform.position.x == gc.Positions[4].position.x && posExit)
        {
            posTableExit = true;
            posi = 5;
        }


    }


    //public void UpdatePos()
    //{

    //    if (Input.GetKeyDown("l") && posi == 2 && gameObject.transform.position.x == gc.Positions[2].position.x)
    //    {


    //        for (int i = 0; i < gc.gc.guestList.Count; i++)
    //        {
    //            if (gc.gc.guestList[i + 1].transform.position.x == gc.Positions[1].position.x)
    //            {
    //                canMove = false;
    //                break;
    //            }

    //            if (gc.gc.guestList[i + 1].transform.position.x != gc.Positions[1].position.x)
    //            {
    //                canMove = true;
    //            }

    //            if (canMove == true)
    //            {
    //                posi = 1;
    //                canMove = false;
    //            }

    //            //    foreach (GameObject g in gc.gc.guestList)
    //            //{

    //            //    if (g.transform.position.x == gc.Positions[1].position.x)
    //            //    {
    //            //        canMove = false;
    //            //        break;
    //            //    }

    //            //    if (g.transform.position.x != gc.Positions[1].position.x)
    //            //    {
    //            //        canMove = true;
    //            //        Debug.Log("can move: " + canMove);
    //            //    }

    //            //    if (canMove == true)
    //            //    {
    //            //        posi = 1;
    //            //        canMove = false;
    //            //    }

    //            //}


    //            //if (gc.gc.guestList[1].transform.position.x != gc.Positions[1].position.x)
    //            //{
    //            //    Debug.Log("ok3");
    //            //posi = 1;
    //        }


    //    }

    //    if (Input.GetKeyDown("k") && posi == 1 && gameObject.transform.position.x == gc.Positions[1].position.x)
    //    {

    //        for (int i = 0; i < gc.gc.guestList.Count; i++)
    //        {
    //            if (gc.gc.guestList[i].transform.position.x == gc.Positions[0].position.x)
    //            {
    //                canMove = false;
    //                break;
    //            }

    //            if (gc.gc.guestList[i].transform.position.x != gc.Positions[0].position.x)
    //            {
    //                canMove = true;
    //            }

    //            if (canMove == true)
    //            {
    //                posi = 0;
    //                canMove = false;
    //            }

    //        }

    //        //foreach (GameObject g in gc.gc.guestList)
    //        //{

    //        //    if (g.transform.position.x == gc.Positions[0].position.x)
    //        //    {
    //        //        canMove = false;
    //        //        break;
    //        //    }

    //        //    if (g.transform.position.x != gc.Positions[0].position.x)
    //        //    {
    //        //        canMove = true;
    //        //        Debug.Log("can move: " + canMove);
    //        //    }

    //        //    if (canMove == true)
    //        //        {
    //        //            posi = 0;
    //        //        canMove = false;
    //        //        }

    //        //}

    //    }

    //    if (Input.GetKeyDown("n") && posi == 0 && gameObject.transform.position.x == gc.Positions[0].position.x)
    //    {

    //        for (int i = 0; i < gc.gc.guestList.Count; i++)
    //        {
    //            if (gc.gc.guestList[i].transform.position.x == gc.Positions[3].position.x)
    //            {
    //                canMove = false;
    //                break;
    //            }

    //            if (gc.gc.guestList[i].transform.position.x != gc.Positions[3].position.x)
    //            {
    //                canMove = true;
    //            }

    //            if (canMove == true)
    //            {
    //                posi = 3;
    //                canMove = false;
    //            }

    //            //if (gc.gc.guestList[0].transform.position.x != gc.Positions[3].position.x)
    //            //{
    //            //    Debug.Log("ok1");
    //            posi = 3;
    //            //}
    //            //posi = 3;

    //        }
    //    }

    //    //if (Input.GetKeyDown("k") && posi == 0 && gameObject.transform.position.x == gc.Positions[0].position.x)
    //    //{

    //    //    for (int i = 0; i < gc.gc.guestList.Count; i++)
    //    //    {
    //    //        if (gc.gc.guestList[i].transform.position.x == gc.Positions[3].position.x)
    //    //        {
    //    //            canMove = false;
    //    //            break;
    //    //        }

    //    //        if (gc.gc.guestList[i].transform.position.x != gc.Positions[3].position.x)
    //    //        {
    //    //            canMove = true;
    //    //        }

    //    //        if (canMove == true)
    //    //        {
    //    //            posi = 3;
    //    //            canMove = false;
    //    //        }

    //    //        //if (gc.gc.guestList[0].transform.position.x != gc.Positions[3].position.x)
    //    //        //{
    //    //        //    Debug.Log("ok1");
    //    //        //posi = 3;
    //    //        //}
    //    //        //posi = 3;

    //    //    }
    //    //}

    //    if (Input.GetKeyDown("n") && posi == 3 && gameObject.transform.position.x == gc.Positions[3].position.x)
    //    {
    //        posi = 4;

    //    }

    //    if (Input.GetKeyDown("n") && posi == 4 && gameObject.transform.position.x == gc.Positions[4].position.x)
    //    {
    //        posi = 5;

    //    }

    //}

    public void IsPosFree()
    {

        //if(gc.gc.guestList)

        //if (pos0Free)
        //    posi = 0;

        //if (pos1Free)
        //    posi = 0;

        //if (pos2Free)
        //    posi = 0;


    }

   
}
