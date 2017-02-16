using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
	public MapBuilder boardScript;
	public int count;
	public int timer;
	public int dead;
	public Text score;


	// Use this for initialization
	void Start () {

		boardScript = GetComponent<MapBuilder>();
		transform.position = FindFreeLocation(2.0f);
		timer = 30;
		dead = 0;
	}

	// Update is called once per frame
	void Update () {
		if (timer != 0)
			timer--;
		else
		{
			timer = 10;
			float newx = Random.Range(-1.0f, 1.0f);
			float newy = Random.Range(-1.0f, 1.0f);
			Vector3 new_pos = new Vector3(newx, newy, 0.0f);           
			transform.position += new_pos * Time.deltaTime * 5;
			if (transform.position.x > 30 || transform.position.x < 0 || transform.position.y > 30 || transform.position.y < 0)
				transform.position = FindFreeLocation(2.0f);
		}

	}

	//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
	private void OnTriggerEnter2D(Collider2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if (other.tag == "Player")
		{
			transform.position = FindFreeLocation(2.0f);
			if (count > 4)
			{
				GetComponent<Renderer>().enabled = false;
				dead++;
				//score.text = "Monsters Killed" + dead ;
			}
			else
			{
				Debug.Log(count);
				count++;
			}


		}

	}

	public Vector2 FindFreeLocation(float radius)
	{
		// Fill this in with something real.
		//return new Vector2(0, 6);
		while (true)
		{
			int newx = Random.Range(0, 30);
			int newy = Random.Range(0, 30);
			Vector2 new_pos = new Vector2(newx, newy);
			// if (boardScript.cellMap[newx, newy] != true)
			// {
			//     return new_pos;
			// }
			//else
			//{
			Debug.Log("hehe");
			return new_pos;

			//}
		}
	}
}
