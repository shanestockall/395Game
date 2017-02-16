using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public MapBuilder boardScript; 
	private int level = 1; 
	private bool doingSetup = true; 
	private Text levelText;
	private GameObject levelImage;
	private GameObject daveImage;



	// Use this for initialization
	void Awake () {
		boardScript = GetComponent<MapBuilder> (); 
		InitGame (); 
		
	}

	void InitGame(){

		doingSetup = true; 
		boardScript.SetupScene (level);
	}

	//Hides black image used between levels
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);

		//Set doingSetup to false allowing player to move again.
		doingSetup = false;
	}


	// Update is called once per frame
	void Update () {
		
	}
}
