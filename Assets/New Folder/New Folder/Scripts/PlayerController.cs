using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed;
	public float jumpHeight;

	bool facingRight;
	bool grounded;

	Rigidbody2D myBody;
	Animator myAnim;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();

		facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");

		myAnim.SetFloat ("speed", Mathf.Abs (move));

		myBody.velocity = new Vector2 (move * maxSpeed, myBody.velocity.y);

		if (move > 0 && !facingRight) {
			flip ();
		} else if (move < 0 && facingRight) {
			flip ();
		}

		if (Input.GetKey (KeyCode.Space)) {
			if (grounded) {
				grounded = false;
				myBody.velocity = new Vector2 (myBody.velocity.x, jumpHeight); 
			}
		}
	}
		
	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Ground") {
			grounded = true;
		}
	}
}
