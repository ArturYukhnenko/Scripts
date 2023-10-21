using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashQueue : MonoBehaviour
{

    [SerializeField] Transform[] Positions;
    [SerializeField] float ObjectSpeed;

    int NextPosIndex;
    Transform NextPos;


    void Start()
    {
        NextPos = Positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("nextposindex: "+NextPosIndex);
        Debug.Log("nextpos"+ NextPos);
        MoveGameObject();
    }


    void MoveGameObject()
    {
        if(transform.position.x == NextPos.position.x) //)
        {

            if (Input.GetKeyDown(KeyCode.N))
            NextPosIndex++;

            if(NextPosIndex >= Positions.Length)
            {
                NextPosIndex = 0;
            }

            if (transform.position.x == Positions[5].position.x)
                Destroy(gameObject);

            NextPos = Positions[NextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPos.position, ObjectSpeed * Time.deltaTime);
        }
    }
}
