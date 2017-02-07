using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements control of the player
/// </summary>
public class PlayerController : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    ///  Called once per grade
    /// </summary>
    internal void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {   
            transform.position += transform.up.normalized * 2 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.position -= transform.up.normalized * 2 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position -= transform.right.normalized * 2 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right.normalized * 2 * Time.deltaTime;
        }

    }

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Oops");
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Wall")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {

                transform.position -= transform.up.normalized * 2 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {

                transform.position -= transform.right.normalized * 2 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += transform.right.normalized * 2 * Time.deltaTime;
            }

        }

    }

}
