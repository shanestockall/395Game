using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public int modifier; 
	public Stat statEffect; 
	public string name; 
	public Sprite sprite; 

	public Item(){ 
		name = "unknown"; 
		modifier = 0; 
		sprite = null; 
		statEffect = null; 
	}

	public Item(string n, int m, Sprite s, Stat se){
		name = n; 
		modifier = m; 
		sprite = s; 
		statEffect = se; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
