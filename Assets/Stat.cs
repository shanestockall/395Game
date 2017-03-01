using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour {


	public int value;
	public string name; 
	public int modifier; 

	public Stat(){ 
		name = "unknown"; 
		value = 0; 
		modifier = 0; 
	}

	public Stat(string n, int v, int m){
		name = n; 
		value = v; 
		modifier = m; 
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
