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

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {   
            transform.position += transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

            transform.position -= transform.up.normalized * speed * Time.deltaTime;
        }

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {

            transform.position -= transform.right.normalized * -speed * Time.deltaTime;
			if (!isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 180f, 0f);
				isFlipped = true;
			}
        }

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right.normalized * speed * Time.deltaTime;
			if (isFlipped) {
				transform.localRotation = Quaternion.Euler (0f, 0f, 0f);
				isFlipped = false;
			}
        }

		if (Input.GetKeyDown (KeyCode.J) && Time.time > nextAttack && energy.value >= 20) {
			animator.SetTrigger ("playerChop");
			energy.value -= 20; 
			nextAttack = Time.time + attackCooldown;
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
			foreach(GameObject g in gameOverObjects){
				g.SetActive (true); 
		}
    }
	}

	internal void Update() { 
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
	}
		

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
			if (other.gameObject.GetComponent<EnemyController>().ded == true && Time.time >= nextAttack)
            {
                animator.SetTrigger("playerChop");
                other.gameObject.SetActive(false);
                dead++;
                score.text = "Monsters Killed: " + dead;
				energy.value -= 10; 
				nextAttack = Time.time + attackCooldown; 
            }
			            
            if (dead == 5)
            {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
            }
        }
			

        if (other.gameObject.tag == "berry")
        {

            if (other.gameObject.GetComponent<berryManager>().collected == true)
            {
                animator.SetTrigger("playerChop");
                other.gameObject.SetActive(false);
                berries++;
                score.text = "Berries Collected: " + berries;
            }
           

            if (berries == 5)
            {
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
            }
            
        }

		if (other.gameObject.tag == "key")
		{
			animator.SetTrigger("playerChop");
			keys++;
			score.text = "Keys Collected: " + keys;
			energy.value -= 10; 

			if (keys == 1)
			{
				foreach (var go in GameObject.FindGameObjectsWithTag("exitwall")) {
					go.SetActive (false);
				}
			}

		}

		if (other.gameObject.tag == "exit") {
			SceneManager.LoadScene (sm.scene);
			var wallList = GameObject.FindGameObjectsWithTag ("exitwall"); 
			foreach (var go in wallList) {
				go.SetActive (true); 
			}
		}
			
    }


	IEnumerator FlashSprites(SpriteRenderer sprite, int numTimes){ 
		Color32 myColor = new Color32(255, 0, 0, 255);
		for(var n = 0; n < numTimes; n++)
		{
			sprite.color = Color.white;
			yield return new WaitForSeconds(0.1f);
			sprite.color = myColor;
			yield return new WaitForSeconds(0.1f);
		}
		sprite.color = Color.white;
	}

}
