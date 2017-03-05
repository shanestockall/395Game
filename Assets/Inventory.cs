using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Mostly from unity adventure tutorial 
// https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/inventory?playlist=44381

public class Inventory : MonoBehaviour {

	public Image[] itemImages = new Image[numItemSlots]; 
	public Item[] items = new Item[numItemSlots]; 
	public const int numItemSlots = 4; 

	public void AddItem(Item itemToAdd){

		for (int i = 0; i < items.Length; i++) { 
			if (items [i] == null) { 
				items [i] = itemToAdd; 
				itemImages [i].sprite = itemToAdd.sprite; 
				itemImages [i].enabled = true; 
				return; 
			}
		}
	}

	public void RemoveItem (Item itemToRemove) { 
		for(int i = 0; i < items.Length; i++){
			if (items [i] == itemToRemove) {
				items [i] = null; 
				itemImages[i].sprite = null; 
				itemImages [i].enabled = false; 
				return;
			}
		}
	}
	
		


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
