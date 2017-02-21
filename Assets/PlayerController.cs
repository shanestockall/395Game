﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Implements control of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public EnemyController ec;
    public sceneMan sm;
    int count = 0;  

	public bool isFlipped = false; 

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
        
        if (ec.dead == 5)
        {
            SceneManager.LoadScene(sm.scene);

        }
            count++;


    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
       Rigidbody2D rigi = GetComponent<Rigidbody2D>();
        Vector2 right = transform.right.normalized;

        //Check if the tag of the trigger collided with is Exit.
		if (other.tag == "Wall") {
           
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.position += transform.up.normalized * 0 * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
            
				transform.position -= transform.up.normalized * 0 * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.LeftArrow)) {

				rigi.velocity -= right * 0 * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				rigi.velocity += right * 0 * Time.deltaTime;
			}

		} else {
			animator.SetTrigger ("playerChop");
		}
			
    }

}
