using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	// public GameObject rigidbody2DExtension;
	public float moveSpeed = 10f;
	public float orbitRadius = 5f;
	Vector3 player = new Vector3(0,0,0);
	public float shrinkRate = 0.2f;
	public float enemyScale = 1f;

	// Use this for initialization
	void Start () {
		// shrink(-enemyScale);

	}

	// Update is called once per frame
	void Update () {
		if (transform.position.magnitude >= orbitRadius) {
			GetComponent<Rigidbody2D>().AddForce(-1 * transform.position * moveSpeed * (transform.position.magnitude-orbitRadius));
		}
	}

	void OnCollisionEnter2D( Collision2D col) {
		if (col.gameObject.name == "Bullet(Clone)") {
			if(transform.localScale.x > 0.5f){
				GetComponent<Rigidbody2D>().AddForce(-2 * transform.position );
				shrink(shrinkRate);
			}
			else {
				while(transform.localScale.x > 0) {
					shrink(shrinkRate/10f);
				}
				Die();
			}
		}
		else if(col.gameObject.name == "Enemy(clone)"){
			shrink(-shrinkRate);
		}
	}
	void Die() {
		Destroy(gameObject);
	}
	void ChangeColor() {
    GetComponent<SpriteRenderer>().color = Random.ColorHSV();
	}
	IEnumerator slowShrink() {
		shrink(-shrinkRate);
    yield return new WaitForSeconds(1);
	}
	public void shrink(float rate) {
		float currentSize = transform.localScale.x;
		transform.localScale = new Vector2 (currentSize - rate,currentSize - rate);
		GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass - rate;
	}

}
