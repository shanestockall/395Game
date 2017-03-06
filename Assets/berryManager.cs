using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berryManager : MonoBehaviour {
    public bool collected; 
	// Use this for initialization
	void Start () {
        transform.position = FindFreeLocation();
        collected = false;
    }
	



    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionEnter2D(Collision2D other)
    {
		//Check if the tag of the trigger collided with is Exit.
		if (other.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
			collected = true; 
		}

    }

    public Vector2 FindFreeLocation()
    {
        // Fill this in with something real.
        //return new Vector2(0, 6);
        //bool[,] cellM;

        while (true)
        {

            int newx = Random.Range(0, 30);
            int newy = Random.Range(0, 30);
            Vector2 new_pos = new Vector2(newx, newy);
            //return new_pos;
            if (Physics2D.OverlapCircle(new_pos, 2) == null)
            {
                return new_pos;
            }

            /*
            cellM = boardScript.cellM;
            
            if (cellM[newx, newy] != true)
			{
			     return new_pos;
			 }
           /* else
            {
                FindFreeLocation();
            }  */
        }
    }
}
