using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public GameManager boardScript;
    public int count;
	public int timer;
	public int dead;
    public bool ded;
	public Text score;
    public GameObject playerObject;
    public Rigidbody2D rigi;


    void Awake()
    {
        
        
        GameObject g = GameObject.Find("GameManager");
        boardScript = g.GetComponent<GameManager>();
        rigi = GetComponent<Rigidbody2D>();


    }
    // Use this for initialization
    void Start () {
		transform.position = FindFreeLocation();
		timer = 30;
		
	}


	// Update is called once per frame
	void Update () {
        Vector2[] directions = { transform.right.normalized, transform.up.normalized, -1 * transform.right.normalized, -1 * transform.up.normalized };
        int rand;

        if (timer != 0)
			timer--;
		else
		{
			timer = 30;
            rand = Random.Range(0, directions.Length);
            rigi.velocity += directions[rand] * Time.deltaTime * 5;
			if (transform.position.x > 30 || transform.position.x < 0 || transform.position.y > 30 || transform.position.y < 0)
				transform.position = FindFreeLocation();
            
		}

	}

 

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionEnter2D(Collision2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if (other.gameObject.tag == "Player")
		{
            if (Input.GetKey(KeyCode.Z)){
                ded = true;
            }
            
			//transform.position = FindFreeLocation();

            //gameObject.SetActive(false);

		}

	}
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Z))
            {
                ded = true;
            }
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
