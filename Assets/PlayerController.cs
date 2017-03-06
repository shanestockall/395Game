using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Implements control of the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator animator;
    EnemyController ec;
    sceneMan sm;
    public Text score;
    int count = 0;
    int dead = 0;
    int berries = 0;
    int keys = 0;
    float speed; 
	public int gameType; 

	// stats stuff
	Stat[] stats;
	GameObject[] gameOverObjects; 
	GameObject[] pauseObjects; 
	public Stat[] startStats = new Stat[5]; 
	public int totalStat; 
	public int statsLeft;
	public float startHealth; 
	public float startEnergy; 
	public int startStrength; 
	public int startDexterity; 
	public int startLuck; 
	public Stat health; 
	public Stat energy; 
	public Stat strength; 
	public Stat dexterity; 
	public Stat luck; 
	public int healthCount;
	public int energyCount;
	public int strengthCount;
	public int dexterityCount;
	public int luckCount;
	private Canvas canv;
	private int energyCounter; 
	private int sprintCounter; 
	public float nextAttack;
	private float attackCooldown; 
	private bool playerDead; 
	GameObject statObject;
	GameObject invObject; 
	public int numHP; 
	public int numEnergy; 
	public List<int> broadSwordList;
	public List<int> woodSwordList; 
	//public List<int> bowList; 
	//public int numArrows; 
	//public GameObject arrowPrefab; 
	//public Transform arrowSpawn; 


	public bool pause = false; 
	public bool isFlipped = false;
    public bool monsters = false;
	public bool sprinting = false; 

	public GameManager gm;  

	void Awake() { 
		totalStat = Random.Range (20, 35); 
		statsLeft = totalStat; 

		healthCount = Random.Range (0, statsLeft); 

		statsLeft -= healthCount; 

		energyCount = Random.Range (0, statsLeft); 

		statsLeft -= energyCount; 

		strengthCount = Random.Range (0, statsLeft);

		statsLeft -= strengthCount; 

		dexterityCount = Random.Range (0, statsLeft);

		statsLeft -= dexterityCount; 

		luckCount = statsLeft; 

		startStats [0] = new Stat ("Health", 100+ healthCount * 10, 1);
		startStats [1] = new Stat ("Energy", 100 + energyCount * 10, 1);
		startStats [2] = new Stat ("Strength", strengthCount, 1);
		startStats [3] = new Stat ("Dexterity", dexterityCount, 1);
		startStats [4] = new Stat ("Luck", luckCount, 1);

		health = startStats [0]; 
		startHealth = (float)health.value;
		energy = startStats [1];
		startEnergy = energy.value; 
		strength = startStats [2];
		startStrength = strength.value; 
		dexterity = startStats [3]; 
		startDexterity = dexterity.value; 
		luck = startStats [4];
		startLuck = luck.value; 
	}

    // Use this forinitialization
    void Start()
    {
		gameType = 1; 
        animator = GetComponent<Animator>();
        GameObject g= GameObject.FindWithTag("enemy");
        ec = g.GetComponent<EnemyController>();

        g = GameObject.Find("Scene Manager");
        sm = g.GetComponent<sceneMan>();
		gm = GetComponent<GameManager> ();

        speed = 2f;
		energyCounter = 0;
		sprintCounter = 0; 
		attackCooldown = 1f - 0.1f*dexterity.value;
		if (attackCooldown < 0.1f) { 
			attackCooldown = 0.1f; 
		}
        
		pauseObjects = GameObject.FindGameObjectsWithTag ("Pause");
		foreach (GameObject go in pauseObjects) { 
			go.SetActive(false); 
		}

		gameOverObjects = GameObject.FindGameObjectsWithTag ("Game Over"); 
		foreach (GameObject go in gameOverObjects) { 
			go.SetActive (false); 
		} 

		statObject = GameObject.FindGameObjectWithTag ("Stats Text"); 
		statObject.SetActive (false); 

		invObject = GameObject.FindGameObjectWithTag ("Inventory Text"); 
		invObject.SetActive (false); 


		//arrowSpawn = GameObject.FindGameObjectWithTag ("arrowspawn").transform;

    }


    /// <summary>
    ///  Called once per grade
    /// </summary>
    internal void FixedUpdate()
    {
		
		if (Input.GetKey(KeyCode.LeftShift) && energy.value > 10){
			if (speed < 2*(float)1.8)
            {
				speed *= (float)1.8;
				sprinting = true; 
		 
            }
        }
		if (Input.GetKeyUp(KeyCode.LeftShift) && sprinting == true)
        {
            if (speed > 2)
            {
				speed /= (float)1.8;
				sprinting = false; 
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {   
            transform.position += transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.position -= transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position -= transform.right.normalized * -speed * Time.deltaTime;
			if (!isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 180f, 0f);
				isFlipped = true;
			}
        }

		if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += transform.right.normalized * speed * Time.deltaTime;
			if (isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
				isFlipped = false;
			}
        }
			
	}

	internal void Update() { 

		if (gameType == 1) { 
			score.text = "Monsters Killed: 0";
		}
		if (gameType == 2) { 
			score.text = "Berries Collected: 0";
		}
		if (gameType == 3) { 
			score.text = "Find the key!"; 
		} 
		if (gameType == 4) {
			score.text = "Solve the puzzle!"; 
		} 
		if (gameType == 5) { 
			score.text = "Kill the boss!"; 
		}

		if (Input.GetKeyDown(KeyCode.Escape)) { 
			pause = !pause; 

			if (pause)
			{
				Time.timeScale = 0.0f; 
				foreach (GameObject g in pauseObjects){
				g.SetActive(true); 
				}
			}
			else { 
				Time.timeScale = 1.0f; 
				foreach (GameObject g in pauseObjects) { 
					g.SetActive(false); 
				}
			}
		}

		if (health.value <= 0) { 
			foreach (GameObject go in gameOverObjects) { 
				go.SetActive (true); 
			} 

			Time.timeScale = 0.0f; 
		}

		if (Input.GetKeyDown(KeyCode.R)) { 
			SceneManager.LoadScene (0);
			Time.timeScale = 1.0f; 
		}

		transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);

		if (Input.GetKeyDown (KeyCode.S)) { 
			if (statObject.activeSelf) {
				statObject.SetActive (false);
			} else
				statObject.SetActive (true);
		}

		if (Input.GetKeyDown (KeyCode.I)) { 
			if (invObject.activeSelf) {
				invObject.SetActive (false);
			} else
				invObject.SetActive (true); 
		}

		statObject.GetComponent<Text> ().text = "Player Stats\nStr: " + strength.value.ToString() + "\nDex: " + dexterity.value.ToString() + "\nLuk: " + luck.value.ToString() + "\nVit: " + (startHealth / 10).ToString() + "\nEnd: " + (startEnergy / 10).ToString() + "\nHealth: " + health.value.ToString() + "\nEnergy: " + energy.value.ToString(); 
		invObject.GetComponent<Text> ().text = "Inventory\nHP Potions: " + numHP.ToString () + "\nEnergy Potions: " + numEnergy.ToString () + "\nWeapons: " + GetInventory (); //"\nArrows: " + numArrows.ToString ();

		if (Input.GetKeyDown (KeyCode.Z) && Time.time > nextAttack && energy.value >= 20) {
			animator.SetTrigger ("playerChop");
			energy.value -= 20; 
			nextAttack = Time.time + attackCooldown;
		}

		/*if (Input.GetKeyDown (KeyCode.X) && Time.time > nextAttack && energy.value >= 40) { 
			ShootArrow(); 
			energy.value -= 40; 
			nextAttack = Time.time + attackCooldown; 
		}*/

		if (Input.GetKeyDown (KeyCode.C) && numHP > 0) {
			if (health.value + 50 <= startHealth * 10) {
				health.value += 50;
			} else
				health.value = (int)(startHealth * 10); 
			numHP--; 
		}

		if (Input.GetKeyDown (KeyCode.V) && numEnergy > 0) { 
			if (energy.value + 50 <= startEnergy * 10) {
				energy.value += 50;
			} else
				energy.value = (int)(startEnergy * 10); 
			
			numEnergy--; 
		}

		if (energy.value != startEnergy && !sprinting) { 
			energyCounter += 1; 
			if (energyCounter == 100) {
				energy.value += 10; 
				energyCounter = 0; 
			}
		}

		if (sprinting) { 
			sprintCounter += 1; 
			if (sprintCounter == 30) { 
				energy.value -= 10; 
				sprintCounter = 0; 
			}
		}

		if (energy.value < 10) { 
			sprinting = false; 
			if (speed > 2) { 
				speed /= 1.8f; 
			}
		} 

		if (health.value <= 0) {
			playerDead = true; 
		}

		if (playerDead == true) {
			foreach (GameObject g in gameOverObjects) {
				g.SetActive (true); 
			}
		}
	}
		

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "boss") {
			if (other.gameObject.GetComponent<BossController> ().ded == true && Time.time >= nextAttack) {
				animator.SetTrigger ("playerChop");
				other.gameObject.SetActive (false);
				dead++;
				score.text = "Kill the boss!";
				energy.value -= 10; 
				nextAttack = Time.time + attackCooldown; 
			}

			if (other.gameObject.GetComponent<BossController> ().health <= 0) {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}
				
		}

		if (other.gameObject.tag == "enemy") {
			if (other.gameObject.GetComponent<EnemyController> ().ded == true && Time.time >= nextAttack) {
				animator.SetTrigger ("playerChop");
				other.gameObject.SetActive (false);
				dead++;
				if (gameType == 1) {
					score.text = "Monsters Killed: " + dead;
				}
				energy.value -= 10; 
				nextAttack = Time.time + attackCooldown; 
			}
			            
			if (dead == 5) {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}
		}
			

		if (other.gameObject.tag == "berry" && gameType == 2) {

			if (other.gameObject.GetComponent<berryManager> ().collected == true) {
				animator.SetTrigger ("playerChop");
				other.gameObject.SetActive (false);
				berries++;
				score.text = "Berries Collected: " + berries;
			}
           

			if (berries == 5) {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}
            
		}

		if (other.gameObject.tag == "key" && gameType == 3) {
			animator.SetTrigger ("playerChop");
			keys++;
			score.text = "Keys Collected: " + keys;
			energy.value -= 10; 

			if (keys == 1) {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}

		}

		if (other.gameObject.tag == "exit") {
			gameType = 5; 
			SceneManager.LoadScene (4);
			var wallList = GameObject.FindGameObjectsWithTag ("exitwall"); 
			foreach (var go in wallList) {
				go.SetActive (true); 
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other){
		/*	
		if (other.gameObject.tag == "arrow") {
				numArrows++;  
				Destroy (other.gameObject);
			}
		*/
		/*
			if (other.gameObject.tag == "bow") {
				weaponsList.Add ("Bow", 1); 
				Destroy (other.gameObject);
			}
		*/
			if (other.gameObject.tag == "broadsword") {
				broadSwordList.Add (1);
				strength.value += 10 * broadSwordList.Count; 
				Destroy (other.gameObject);
			}
			if (other.gameObject.tag == "woodsword") {
				woodSwordList.Add (1);
				strength.value += 5 * woodSwordList.Count;
				Destroy (other.gameObject);
			}
			if (other.gameObject.tag == "healthpotion") {
				numHP++;
				Destroy (other.gameObject); 
			}
			if (other.gameObject.tag == "energypotion") {
				numEnergy++;
				Destroy (other.gameObject);
			}
    }
		

	private string GetInventory(){
		string output = ""; 
	
		if (broadSwordList.Count == 0 && woodSwordList.Count == 0) { 
			return "None";
		}

		if (broadSwordList.Count > 0) { 
			output += ("\nBroad Sword x" + broadSwordList.Count.ToString()); 
		}

		if (woodSwordList.Count > 0) { 
			output += ("\nWood Sword x" + woodSwordList.Count.ToString ());
		}
			
		return output; 
	}
	/*
	public void ShootArrow() { 
		GameObject Clone; 
		Clone = (Instantiate (arrowPrefab, arrowSpawn)) as GameObject;

		Clone.GetComponent<Rigidbody2D> ().velocity = Clone.transform.forward * 5f; 

		Destroy (Clone, 3f); 
	}
   */
}
