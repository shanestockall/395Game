using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthBarController : MonoBehaviour {
	
	GameObject p;
	PlayerController pc;
	Canvas c;
	Image[] bars; 
	Image healthBar; 
	Image energyBar;

	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag("Player");
		pc = p.GetComponent<PlayerController>(); 
		c = GameObject.FindGameObjectWithTag ("canvas").GetComponent<Canvas>();
		bars = c.GetComponentsInChildren<Image>();
		healthBar = bars [0];
		energyBar = bars [1]; 
	}

	// Update is called once per frame
	void Update () {

		healthBar.fillAmount =  pc.health.value / pc.startHealth;
		energyBar.fillAmount = pc.energy.value / pc.startEnergy;
		
	}
}
