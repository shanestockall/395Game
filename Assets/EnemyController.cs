using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public MapBuilder boardScript;
    public int count; 

    // Use this for initialization
    void Start () {
        boardScript = GetComponent<MapBuilder>();
        Debug.Log(transform.position);
        transform.position = FindFreeLocation(2.0f);
    }
	
	// Update is called once per frame
	void Update () {
        float newx = Random.Range(-2, 2);
        float newy = Random.Range(-2, 2);
        Vector3 new_pos = new Vector3(newx, newy, 0.0f);
        //transform.position += new_pos;
	}

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Player")
        {
            transform.position = FindFreeLocation(2.0f);
            if (count > 5)
                GetComponent<Renderer>().enabled = false;
            else
            {
                Debug.Log(count);
                count++;
            }

            
        }

    }

    public static Vector2 FindFreeLocation(float radius)
    {
        // Fill this in with something real.
        //return new Vector2(0, 6);
        while (true)
        {
            float newx = Random.Range(0, 30);
            float newy = Random.Range(0, 30);
            Vector2 new_pos = new Vector2(newx, newy);

            if (Physics2D.OverlapCircle(new_pos, radius) == null)
            {
                return new_pos;
            }
        }
    }
}
