using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour {

    //private CGameManager gm;
    
    // Use this for initialization
	void Start ()
    {
        //gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<CGameManager>();
        Vector3 startingPos = new Vector3(8.6f, 5, -10);
        
        gameObject.transform.position = startingPos;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (gm.CurrentHeight > transform.position.y)
        //{
        //    transform.position += new Vector3(0f, gm.CurrentHeight - transform.position.y, 0f);
        //}
	}

    //public void UpdatePosition(int addedHeight)
    //{

    //}
}
