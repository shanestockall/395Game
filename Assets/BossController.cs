using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {
    public GameManager boardScript;
    public int count;
	public int timer;
	public int dead;
    public bool ded;
	public Text score;
    public GameObject playerObject;
    public Rigidbody2D rigi;
	public float health; 
	int attackTimer;
	private Animator animator; 
	private SpriteRenderer renderer;



    void Awake()
    {
        
		health = 300f; 
        GameObject g = GameObject.Find("GameManager");
        boardScript = g.GetComponent<GameManager>();
        rigi = GetComponent<Rigidbody2D>();


    }
    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator>();
		transform.position = FindFreeLocation();
		timer = 30;

		attackTimer = 0; 
		renderer = transform.gameObject.GetComponent<SpriteRenderer> ();
		
	}


	// Update is called once per frame
	void Update () {
        Vector2[] directions = { transform.right.normalized, transform.up.normalized, -1 * transform.right.normalized, -1 * transform.up.normalized };
        int rand;

		if (Mathf.Abs(Vector3.Distance(transform.position, GameObject.Find("Player").transform.position)) < 1){

			attackTimer += 1; 


			if (attackTimer >= 100) {
				GameObject.Find ("Player").GetComponent<PlayerController> ().health.value -= 40; 
				animator.SetTrigger ("enemyAttack");
				StopCoroutine (FlashEnemySprites (transform.gameObject.GetComponent<SpriteRenderer> (), 3)); 
				StopCoroutine (FlashCharacterSprites (GameObject.Find ("Player").GetComponent<SpriteRenderer> (), 3));
				StartCoroutine (FlashCharacterSprites (GameObject.Find ("Player").GetComponent<SpriteRenderer> (), 3));

				attackTimer = 0; 
			}
			}

        if (timer != 0)
			timer--;
		else
		{
			timer = 0;

			if (Mathf.Abs(Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position)) <= 5)
			{
				Debug.Log("here");
				transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, 1.5f * Time.deltaTime);
				if (transform.position.x > 30 || transform.position.x < 0 || transform.position.y > 30 || transform.position.y < 0)
					transform.position = FindFreeLocation();
			}

        }

	}

 

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnCollisionEnter2D(Collision2D other)
	{
		//Check if the tag of the trigger collided with is Exit.
		if (other.gameObject.tag == "Player")
		{
			PlayerController pc = other.gameObject.GetComponent<PlayerController> (); 
			float nextAttack = pc.nextAttack; 
			float energyVal = pc.energy.value; 
			if (Input.GetKey(KeyCode.Z) && Time.time >= nextAttack && energyVal >= 20){
				StopCoroutine (FlashCharacterSprites (GameObject.Find ("Player").GetComponent<SpriteRenderer> (), 3));
				StopCoroutine (FlashEnemySprites (transform.gameObject.GetComponent<SpriteRenderer> (), 3));
				StartCoroutine(FlashEnemySprites(transform.gameObject.GetComponent<SpriteRenderer>(), 3));

				transform.gameObject.GetComponent<BossController>().health -= 10f + other.gameObject.GetComponent<PlayerController>().strength.value;

				Debug.Log (health);
				if (health <= 0f) {
					ded = true;
				}

            }
            
			//transform.position = FindFreeLocation();

            //gameObject.SetActive(false);

		}

	}


    private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			PlayerController pc = other.gameObject.GetComponent<PlayerController> (); 
			float nextAttack = pc.nextAttack; 
			float energyVal = pc.energy.value;
			if (Input.GetKey(KeyCode.Z) && Time.time >= nextAttack && energyVal >= 20) {
				StopCoroutine (FlashCharacterSprites (GameObject.Find ("Player").GetComponent<SpriteRenderer> (), 3));
				StopCoroutine (FlashEnemySprites (transform.gameObject.GetComponent<SpriteRenderer> (), 3)); 
				StartCoroutine(FlashEnemySprites(transform.gameObject.GetComponent<SpriteRenderer>(), 3));
				transform.gameObject.GetComponent<BossController>().health -= 10f + other.gameObject.GetComponent<PlayerController>().strength.value;
				Debug.Log (health);
				if (health <= 0f) {
					ded = true;
				}
			}
		}
	}


    public Vector2 FindFreeLocation()
	{
        // Fill this in with something real.
        //return new Vector2(0, 6);
        //bool[,] cellM;

        while (true)
		{

			int newx = Random.Range(0, 30);
			int newy = Random.Range(0, 30);
			Vector2 new_pos = new Vector2(newx, newy);
            //return new_pos;
            if (Physics2D.OverlapCircle(new_pos, 2) == null)
            {
                return new_pos;
            }

            /*
            cellM = boardScript.cellM;
            
            if (cellM[newx, newy] != true)
			{
			     return new_pos;
			 }
           /* else
            {
                FindFreeLocation();
            }  */
		}
	}

	IEnumerator FlashEnemySprites(SpriteRenderer sprite, int numTimes){ 
		Color32 myColor = new Color32(255, 0, 0, 255);
		for(var n = 0; n < numTimes; n++)
		{
			sprite.color = Color.white;
			yield return new WaitForSeconds(0.05f);
			sprite.color = myColor;
			yield return new WaitForSeconds(0.05f);
		}

		sprite.color = Color.white;
	}

	IEnumerator FlashCharacterSprites(SpriteRenderer sprite, int numTimes){ 
		Color32 myColor = new Color32(255, 0, 0, 255);
		for(var n = 0; n < numTimes; n++)
		{
			sprite.color = Color.white;
			yield return new WaitForSeconds(0.05f);
			sprite.color = myColor;
			yield return new WaitForSeconds(0.05f);
		}

		sprite.color = Color.white;
	}
}
