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


	// Use this for initialization
	void Start () {
		 rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        grounded = false;
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



	void OnCollision2D(Collider2D collision){
		if (collision.gameObject.tag == "Ground"){
			grounded = true;
		}
	}
}
