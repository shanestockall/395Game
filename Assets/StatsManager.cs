using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsManager : MonoBehaviour {

	public Stat[] stats = new Stat[5]; 
	public int totalStat; 
	private int statsLeft;

	private int health; 
	private int energy; 
	private int strength; 
	private int dexterity; 
	private int luck; 

	// Use this for initialization
	void Start () {
		
		totalStat = Random.Range (20, 35); 
		statsLeft = totalStat; 

		health = Random.Range (0, statsLeft); 

		statsLeft -= health; 

		energy = Random.Range (0, statsLeft); 

		statsLeft -= energy; 

		strength = Random.Range (0, statsLeft);

		statsLeft -= strength; 

		dexterity = Random.Range (0, statsLeft);

		statsLeft -= dexterity; 

		luck = statsLeft; 

		stats [0] = new Stat ("Health", 100 + health * 10, 1);
		stats [1] = new Stat ("Energy", 100 + energy * 10, 1);
		stats [2] = new Stat ("Strength", strength, 1);
		stats [3] = new Stat ("Dexterity", dexterity, 1);
		stats [4] = new Stat ("Luck", luck, 1);

	}

	void Awake() {


	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public Stat[] getStats(){
		return stats; 
	}
}
