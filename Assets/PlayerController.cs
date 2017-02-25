using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Implements control of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator animator;
    EnemyController ec;
    sceneMan sm;
    public Text score;
    int count = 0;
    int dead = 0;
    int berries = 0;
    int keys = 0;
    int speed; 

	public bool isFlipped = false;
    public bool monsters = false;

	public GameManager gm; 


    // Use this forinitialization
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject g= GameObject.FindWithTag("enemy");
        ec = g.GetComponent<EnemyController>();

        g = GameObject.Find("Scene Manager");
        sm = g.GetComponent<sceneMan>();
		gm = GetComponent<GameManager> ();

        speed = 5;

        
    }


    /// <summary>
    ///  Called once per grade
    /// </summary>
    internal void FixedUpdate()
    {
       
        if (Input.GetKeyDown(KeyCode.M)){
            if (speed < 10)
            {

                speed *= 2;
                Debug.Log(speed);
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (speed > 5)
            {
                speed /= 2;
                Debug.Log(speed);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {   
            transform.position += transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

            transform.position -= transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {

            transform.position -= transform.right.normalized * -speed * Time.deltaTime;
			if (!isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 180f, 0f);
				isFlipped = true;
			}
        }

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right.normalized * speed * Time.deltaTime;
			if (isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
				isFlipped = false;
			}
        }
    }
		

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>().ded == true)
            {
                animator.SetTrigger("playerChop");
                other.gameObject.SetActive(false);
                dead++;
                score.text = "Monsters Killed: " + dead;
            }
            
            
            if (dead == 5)
            {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
            }
        }

            
        

        if (other.gameObject.tag == "berry")
        {

            if (other.gameObject.GetComponent<berryManager>().collected == true)
            {
                animator.SetTrigger("playerChop");
                other.gameObject.SetActive(false);
                berries++;
                score.text = "Berries Collected: " + berries;
            }
           

            if (berries == 5)
            {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
            }
            
        }

		if (other.gameObject.tag == "key")
		{
			animator.SetTrigger("playerChop");
			keys++;
			score.text = "Keys Collected: " + keys;

			if (keys == 1)
			{
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}

		}

		if (other.gameObject.tag == "exit") {
			SceneManager.LoadScene (sm.scene);
			var wallList = GameObject.FindGameObjectsWithTag ("exitwall"); 
			foreach (var go in wallList) {
				go.SetActive (true); 
			}
		}
    }

}
