using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	int GRID_WIDTH = 50; 
	int GRID_HEIGHT = 50; 
	float CHANCE_TILE_LIVE = 0.45f;
	GameObject[] floorTiles;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool[,] initializeMap(bool[,] map){
		for (int x = 0; x < GRID_WIDTH; x++) {
			for (int y = 0; y < GRID_HEIGHT; y++) { 
				float randomNum = Random.Range (0.0f, 1.0f);
				if (randomNum < CHANCE_TILE_LIVE) { 
					map [x, y] = true; 
				}
			}
		}

		return map; 
	}

	public int countAliveNeighbors(bool[,] map, int x, int y){
		int count = 0; 
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int neighbor_x = x + i;
				int neighbor_y = y + j;

				if (i == 0 && j == 0) {
					//do nothing
				} else if (neighbor_x < 0 || neighbor_y < 0 || neighbor_x >= GRID_WIDTH || neighbor_y >= GRID_HEIGHT) {
					count += 1; 
				} else if (map [neighbor_x, neighbor_y]) {
					count += 1; 
				} 
			}
		}

		return count; 
	} 

	public bool[,] simulationStep(bool[,] oldMap){
		bool[,] newMap = new bool[GRID_WIDTH, GRID_HEIGHT];

		for (int x = 0; x < GRID_WIDTH; x++) {
			for (int y = 0; y < GRID_HEIGHT; y++) {
				int nbs = countAliveNeighbors (oldMap, x, y);

				if (oldMap [x, y]) {
					if (nbs < 3) {
						newMap [x, y] = false;
					} else {
						newMap [x, y] = true;
					}
				} else {
					if (nbs > 3) { 
						newMap [x, y] = true;
					} else {
						newMap [x, y] = false;
					}
				}
			}
		}

		return newMap;
	}

	public bool[,] generateMap(){
		bool[,] cellMap = new bool[GRID_WIDTH, GRID_HEIGHT];
		cellMap = initializeMap (cellMap);
		for (int i = 0; i < 6; i++) {
			cellMap = simulationStep (cellMap);
		}

		return cellMap;
	}
}
