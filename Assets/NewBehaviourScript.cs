using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    bool MiniMapEn = true;
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, -20);
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (MiniMapEn)
            {
                GameObject.FindGameObjectWithTag("Minimap").GetComponent<RawImage>().enabled = false;
                MiniMapEn = false;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Minimap").GetComponent<RawImage>().enabled = true;
                MiniMapEn = true;
            }
        }
    }
}
