using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour {
    [SerializeField] Animator anim;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float maxVelocity;
    [SerializeField] float horzJumpDamping;
	[SerializeField] GameObject fireball;
	[SerializeField] GameObject Mario;
	[SerializeField] GameObject flag;
	[SerializeField] AudioClip [] audioClips;
	[SerializeField] Collider2D [] myColliders;
	private static float xPos = 3.243f;
	private static float yPos = -0.55f;
	private static bool backFromBonus = false;

	Scene scene;

    AudioSource aud;
    bool isGrounded;
    bool jumping = false;
	bool isAlive = true;
	bool isDead = false;
	bool inTransition = false;
	bool downThePipe = false;
	bool downPole = false;
	bool downPoleSound = false;
	bool endTransition = false;
    float axis = 0;
	float xAt ;
	int randSound;
    Rigidbody2D rb;

	void Awake()
	{
		
		scene = SceneManager.GetActiveScene ();

		if (scene.name == "Last") 
		{
			backFromBonus = false;
		}

		if (backFromBonus) 
		{
			transform.localPosition = new Vector3 (xPos, yPos, this.transform.position.z);
		}
	}

	void Start () {
		
        rb = this.GetComponent<Rigidbody2D>();
        aud = this.GetComponent<AudioSource>();
	}
	
	void Update () {


		if (!endTransition) {
			
			if (!isAlive || inTransition) {
				axis = 0;
				transform.localPosition = new Vector3 (xAt, this.transform.position.y, this.transform.position.z);

			} else if (isGrounded) {
				axis = Input.GetAxis ("Horizontal");
			}
        
			if (axis < 0)
				transform.localScale = new Vector3 (-1, 1, 1);
			else if (axis > 0)
				transform.localScale = new Vector3 (1, 1, 1);

			if (isAlive && Input.GetKeyDown (KeyCode.Space) && isGrounded == true && !downPole && !downThePipe) {
				jumping = true;
				isGrounded = false;

			}

			if (isAlive && Input.GetKeyDown (KeyCode.F) && !downPole && !downThePipe) {
				Vector3 offset = new Vector3 (0.15f * transform.transform.localScale.x, 0.075f, 0);
				GameObject fbInst = Instantiate (fireball, transform.position + offset, Quaternion.identity) as GameObject;
				fbInst.GetComponent<Rigidbody2D> ().AddForce (Vector3.right * transform.localScale.x * 100);
				GameObject.Destroy (fbInst, 5.0f);

			}

			if (downThePipe) {

				axis = 0;
				transform.localPosition = new Vector3 (xAt, this.transform.position.y - 0.05f, this.transform.position.z);
				Destroy (Mario, 0.1f);

			}

			if (downPole) {
				axis = 0;

				if (downPoleSound) {
					downPoleSound = false;
					aud.clip = audioClips [4];
					aud.Play ();
				}

				if (this.transform.position.y >= -2.689f) {
					transform.localPosition = new Vector3 (xAt, this.transform.position.y - 0.01f, this.transform.position.z);

				} 

				flag.transform.localPosition = new Vector3 (flag.transform.position.x, flag.transform.position.y - 0.01f, flag.transform.position.z);
				if (flag.transform.position.y <= -2.612f) {

					aud.clip = audioClips [5];
					aud.Play ();
					downPole = false;
					endTransition = true;
					transform.localScale = new Vector3 (1, 1, 1);
					InvokeRepeating ("endLevel", 0f, 0.05f);
				}
			}



			anim.SetFloat ("Speed", Mathf.Abs (axis));
			anim.SetFloat ("Resist", (Mathf.Abs ((Input.GetAxisRaw ("Horizontal") - Input.GetAxis ("Horizontal")))));
			anim.SetBool ("Jumping", !isGrounded);
		}
	}

    void FixedUpdate() {

		if (GameManager.returnTime () == 0) 
		{
			timeRanOut ();
		}

        if (jumping == true) 
		{
			randSound = Random.Range (0, 3);

			aud.clip = audioClips [randSound];

			aud.Play ();
           
            rb.AddForce(new Vector2(0,jumpForce*Time.fixedDeltaTime));
            jumping = false;
        }

		if (!isAlive && !isDead) 
		{
			rb.AddForce(new Vector2(0,20000*Time.fixedDeltaTime));
			isDead = true;
		}

        rb.AddForce(new Vector2(axis*(isGrounded?moveSpeed:moveSpeed/horzJumpDamping)*Time.fixedDeltaTime,0));
    }

    void OnTriggerEnter2D (Collider2D other) {
		if (isAlive && other.tag == "Ground")
            isGrounded = true;

		if (isAlive && (other.tag == "pit" || other.tag == "enemy")) 
		{
			xAt = this.transform.position.x;
			isAlive = false;
			aud.clip = audioClips [3];
			aud.Play ();
			anim.SetBool ("dead", true);
			GameManager.loseLive ();
			backFromBonus = false;
			foreach (Collider2D c in myColliders) 
			{
				c.enabled = false;
			}

			if (GameManager.returnLives () > 0) 
			{
				Invoke ("respawn", 3.5f);
			}

			Destroy (gameObject, 3.5f);



		}

		if (other.tag == "lastScenePipeHorizontal") 
		{
			xAt = this.transform.position.x + 0.05f;
			inTransition = true;
		}

		if (other.tag == "flagPole") 
		{
			xAt = this.transform.position.x;
			downPole = true;
			downPoleSound = true;

		}

		if (other.tag == "Coin") 
		{
			GameManager.destroyedObjs.Add (other.gameObject.name);
			aud.clip = audioClips [6];
			aud.Play ();
			Destroy (other.gameObject);
			GameManager.addCoin ();
		}


    }

	void timeRanOut()
	{
		xAt = this.transform.position.x;
		isAlive = false;
		aud.clip = audioClips [3];
		aud.Play ();
		anim.SetBool ("dead", true);
		GameManager.loseLive ();
		backFromBonus = false;
		foreach (Collider2D c in myColliders) 
		{
			c.enabled = false;
		}
		if (GameManager.returnLives () > 0) 
		{
			Invoke ("respawn", 3.5f);
		}

		Destroy (gameObject, 3.5f);

	}

 
	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "lastScenePipeV") 
		{
			Vector2 direction = (other.transform.position - this.transform.position).normalized;

			/*Debug.DrawRay (this.transform.position, direction, Color.red);

			Debug.DrawRay (this.transform.position, Vector2.up, Color.green);

			Debug.Log ("This on top: " + Vector2.Angle (direction, Vector2.up));*/

			if ((Vector2.Angle (direction, Vector2.up) > 150.0f && (Vector2.Angle (direction, Vector2.up) < 180.0f )) && Input.GetKeyDown (KeyCode.S))
			{
				xAt = this.transform.position.x;
				downThePipe = true;
			}

		}

	}

	void endLevel()
	{		
		anim.SetFloat ("Speed", 0.2f);
		anim.SetFloat ("Resist", 0.0f);

		if (Mario.transform.position.x < 2.5) 
		{	
			transform.Translate (Vector3.right * 2f * Time.deltaTime);
		} 
		else if (Mario.transform.position.x >= 2.5) 
		{
			anim.SetFloat ("Speed", 0.0f);
			Destroy (Mario, 5f);
		}

	}

	public static void setMarioPosition()
	{
		backFromBonus = true;
	}

	void respawn()
	{	
		Debug.Log ("Respawning");
		SceneManager.LoadScene ("Intro");
	}
}
