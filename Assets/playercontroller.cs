using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour {

Rigidbody2D rb;
SpriteRenderer sr;
Animator anim;

bool grounded;
public int speed, jump;

public Text ScoreText;

 private float timer;
 private float score;

public GameObject end;

public AudioClip[] sounds;
	public AudioSource instrument;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
        grounded = false;
        end.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;

	    if (timer > 1f) {

	        score += 1;

	        //We only need to update the text if the score changed.
	        ScoreText.text = score.ToString();

	        //Reset the timer to 0.
	        timer = 0;
	    }

		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
		anim.SetFloat("VelocityY", rb.velocity.y);
		anim.SetFloat("PositionY", transform.position.y);

		//Jump code
		if (Input.GetKeyDown(KeyCode.Space) && grounded){
			rb.velocity = new Vector2(0, jump);
			grounded = false;
			anim.SetBool("Grounded", false);
			anim.SetTrigger("Jump Trigger");
		}
		

	}



	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground"){
			grounded = true;
			GetComponent<Animator>().SetBool("Grounded", true);
		}
		else if (collision.gameObject.tag == "Bone"){
			score += 10;
			Destroy(collision.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "enemy") {
			Time.timeScale = 0f;
			gameover();
		}
	}

	void gameover(){
		playsound(0);
end.SetActive (true);
	}

	void playsound(int index) {
		instrument.clip = sounds[index];
		instrument.Play();
	}

}
