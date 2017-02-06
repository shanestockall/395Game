using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

	int GRID_WIDTH = 30; 
	int GRID_HEIGHT = 30; 
	float CHANCE_TILE_LIVE = 0.35f;
	private Transform boardHolder; 
	private List <Vector3> gridPositions = new List<Vector3> ();
	public GameObject[] floorTiles; 
	public GameObject[] wallTiles;

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

	void BoardSetup() 
	{ 
		boardHolder = new GameObject ("Board").transform; 
		bool[,] cellMap = generateMap ();
		GameObject toInstantiate;

		for (int x = -1; x < GRID_WIDTH + 1; x++) {
			for (int y = -1; y < GRID_HEIGHT + 1; y++) {
				if (x == -1 || x == GRID_WIDTH || y == -1 || y == GRID_HEIGHT)
					toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];
				else if (cellMap [x, y])
					toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				else
					toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];

				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent (boardHolder);
			}
		}
	}

	public void SetupScene (int level)
	{
		BoardSetup (); 
	}
}
