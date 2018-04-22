using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public float shrinkRate = 0.05f;
	public bool isReady = false;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(transform.position.magnitude > 7) {
			GetComponent<Rigidbody2D>().AddForce(-1 * transform.position * (transform.position.magnitude-2) * 0.05f);
		}

	}
	void OnCollisionEnter2D( Collision2D col) {
		if(col.gameObject.name == "Enemy(Clone)"){
			if (transform.localScale.x < 0.1f){
				Die();
			} else {
				shrink(shrinkRate);
			}

		}
	}

	void Die() {
		if (isReady){
			PlayerScript playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
			playerScript.LoadBullet();
		}
		Destroy(gameObject);
	}
	public void shrink(float rate) {
		float currentSize = transform.localScale.x;
		transform.localScale = new Vector2 (currentSize - rate,currentSize - rate);
		GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass - rate;
	}
}
