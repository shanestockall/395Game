using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestManager : MonoBehaviour {
    public bool opened;
    public bool correctChest;
    public GameObject storyGen;
    public Sprite openedSprite;
    int answer;
    // Use this for initialization
    void Start() {
        storyGen = GameObject.Find("Story Generator");
        if (transform.name == "Chest")
        {
            transform.position = FindFreeLocation(0);   
        }
        if (transform.name == "Chest (1)")
        {
            transform.position = FindFreeLocation(1);
        }
        if (transform.name == "Chest (2)")
        {
            transform.position = FindFreeLocation(2);
        }
        opened = false;
    }
	// Update is called once per frame
	void Update ()
    {
        answer = storyGen.GetComponent<SuperBasicGeneratorScript>().riddleNum;
        if (transform.name == "Chest")
        {
            if (answer == 0)
            {
                correctChest = true;
            }
            else correctChest = false;
        }
        if (transform.name == "Chest (1)")
        {
            if (answer == 1)
            {
                correctChest = true;
            }
            else correctChest = false;
        }
        if (transform.name == "Chest (2)")
        {
            if (answer == 2)
            {
                correctChest = true;
            }
            else correctChest = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (opened == false && correctChest == true)
            {
                opened = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
                var wallList = GameObject.FindGameObjectsWithTag("exitwall");
                foreach (var go in wallList)
                {
                    go.SetActive(false);
                }
            }
            else if (opened == false && correctChest == false)
            {
                opened = true;
                GameObject.Find("Player").GetComponent<PlayerController>().health.value -= (int)(0.5 * GameObject.Find("Player").GetComponent<PlayerController>().startHealth);
                gameObject.GetComponent<SpriteRenderer>().sprite = openedSprite;
            }
        }
    }

    public Vector2 FindFreeLocation(int quadrant)
    {
        // Fill this in with something real.
        //return new Vector2(0, 6);
        //bool[,] cellM;

        while (true)
        {
            if (quadrant == 0)
            {
                int newx = Random.Range(15, 30);
                int newy = Random.Range(0, 15);
                Vector2 new_pos = new Vector3(newx, newy);
                //return new_pos;
                if (Physics2D.OverlapCircle(new_pos, 1) == null)
                {
                    return new_pos;
                }
            }
            else if (quadrant == 1)
            {
                int newx = Random.Range(15, 30);
                int newy = Random.Range(15, 30);
                Vector2 new_pos = new Vector3(newx, newy);
                //return new_pos;
                if (Physics2D.OverlapCircle(new_pos, 2) == null)
                {
                    return new_pos;
                }
            }
            else if (quadrant == 2)
            {
                int newx = Random.Range(0, 15);
                int newy = Random.Range(15, 30);
                Vector2 new_pos = new Vector3(newx, newy);
                //return new_pos;
                if (Physics2D.OverlapCircle(new_pos, 2) == null)
                {
                    return new_pos;
                }
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
