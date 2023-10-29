using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class globalCustomer : MonoBehaviour
{

    [SerializeField] public Transform[] Positions;
    //public bool pos3Free;
    //public bool pos2Free;
    //public bool pos1Free;


    //private SpawnCustomer spawnCustomer;
    //public GameObject customer;
    //private GameObject[] customerList;

    [SerializeField] CashQueue cq;
    public List<GameObject> guestList =  new List<GameObject>();
    public List<CashQueue> custList = new List<CashQueue>();
    GameObject[] gs;

    public int pos;


    // Start is called before the first frame update
    void Start()
    {
        //pos3Free = false;
        //pos2Free = false;
        //pos1Free = true;

        //GameObject[] gs = GameObject.FindGameObjectsWithTag("Customer");

        //foreach(GameObject g in gs)
        //{
        //    Debug.Log("customers: " + g.name);
        //}
        //customer = GameObject.FindGameObjectWithTag("Customer");
        //Debug.Log("customer: " + customer);

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

    // Update is called once per frame
    void Update()
    {

        //CanAddGuest();
        //AddGuest(gameObject);
        //ebug.Log("addguest: " + cq.Positions[guestList.IndexOf(gameObject)]);

        

        gs = GameObject.FindGameObjectsWithTag("Customer");

        foreach (GameObject g in gs)
        {
            //Debug.Log("length: " + gs.Length);
            if(!guestList.Contains(g.gameObject))
            guestList.Add(g.gameObject);
            pos = guestList.IndexOf(g.gameObject);
            Debug.Log("pos: " + pos);
            //Debug.Log("wew: " + guestList.IndexOf(g.gameObject));
            //Debug.Log("customers: " + g.name);
            //Debug.Log("length: " + gs.Length);
           // Debug.Log("length1: " + guestList.Count);
            //Debug.Log("length2: " + guestList.Count.ToString());
            //Debug.Log("position: "+ g.gameObject.)
        }
        

        //if (gs[0].transform.position.x == cq.NextPos.position.x && transform.position.z == cq.NextPos.position.z)
        //{
        //    cq.pos1Free = false;
        //    cq.pos2Free = true;
        //}

        //Debug.Log("x1"+guestList.Count.ToString());
        //Debug.Log("x2" + gs.Length);
        //Debug.Log(guestList.Count);
        //Debug.Log(guestList.ToString());


        //if (gs[0].gameObject.transform.position.x)//cq.NextPos.position.x)
        //{

        //    Debug.Log("xxx");

        //}

        //Debug.Log("x1"+gs[0].gameObject.transform.position.x);
        //Debug.Log("x2"+gs[0].gameObject.transform.position.x.ToString());
        //Debug.Log("cq: " + Positions[0].position.x);




        //Debug.Log("customer: " + customer);
    }


    //public bool CanAddGuest()
    //{

    //    return guestList.Count < cq.Positions.Count;
    //}


    public void AddGuest(GameObject guest)
    {
        
        //cq.MoveGameObject(cq.Positions[guestList.IndexOf(guest)]);
    }


}
