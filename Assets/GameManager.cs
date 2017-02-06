using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public MapBuilder boardScript; 
	private int level = 1; 

	// Use this for initialization
	void Awake () {
		boardScript = GetComponent<MapBuilder> (); 
		InitGame (); 
		
	}

	void InitGame(){
		boardScript.SetupScene (level);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
