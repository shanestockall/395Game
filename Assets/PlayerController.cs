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

	public bool isFlipped = false;
    public bool monsters = false;

    // Use this forinitialization
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject g= GameObject.FindWithTag("enemy");
        ec = g.GetComponent<EnemyController>();

        g = GameObject.Find("Scene Manager");
        sm = g.GetComponent<sceneMan>();
        
    }


    /// <summary>
    ///  Called once per grade
    /// </summary>
    internal void FixedUpdate()
    {
    

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {   
            transform.position += transform.up.normalized * 5 * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

            transform.position -= transform.up.normalized * 5 * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {

            transform.position -= transform.right.normalized * -5 * Time.deltaTime;
			if (!isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 180f, 0f);
				isFlipped = true;
			}
        }

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right.normalized * 5 * Time.deltaTime;
			if (isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
				isFlipped = false;
			}
        }
        
       
            


    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            animator.SetTrigger("playerChop");
            if (monsters)
            {
                dead++;
                score.text = "Monsters Killed: " + dead;
            
                if (dead == 5)
                {
                    SceneManager.LoadScene(sm.scene);
                }
            }

            
        }

        if (other.gameObject.tag == "berry")
        {
            animator.SetTrigger("playerChop");
            dead++;
            score.text = "Berries Collected: " + dead;

            if (dead == 5)
            {
                SceneManager.LoadScene(sm.scene);
            }

        }
        }

}
