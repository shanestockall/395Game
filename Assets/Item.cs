using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int modifier; 
	public string name; 
	public Sprite sprite; 

	public Item(){ 
		name = "unknown"; 
		modifier = 0; 
		sprite = null; 
	}

	public Item(string n, int m, Sprite s){
		name = n; 
		modifier = m; 
		sprite = s; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
