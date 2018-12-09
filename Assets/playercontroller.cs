using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour {

Rigidbody2D rb;
SpriteRenderer sr;

bool grounded;
public int speed, jump;

public Text ScoreText;

 private float timer;
 private float score;

public GameObject end;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

		if (Input.GetKeyDown(KeyCode.Space) && grounded){
			rb.velocity = new Vector2(0, jump);
			grounded = false;
		}
		

	}



	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground"){
			grounded = true;
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
end.SetActive (true);
	}
}
