using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float shrinkRate = 0.05f;
	public GameObject bulletPreFab;
	public float bulletSpeed = 100;
	public Vector3 direction;
	bool isLoaded = false;
	GameObject bullet;
	public float loadedBulletRadius = 0.2f;
	float currentRadius = 0.5f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(isLoaded){
			direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			direction = (Vector2)(direction);
			direction.Normalize();
			direction = direction * (loadedBulletRadius + currentRadius);
			bullet.transform.position = new Vector3(direction.x, direction.y, 0);
			if (Input.GetMouseButtonDown(0)) {
				fireBullet();
				isLoaded = false;
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject enemy in enemies) {
					EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
					enemyScript.shrink(-0.1f);

				}
			}
		} else {
			LoadBullet();
		}
	}
	void OnCollisionEnter2D( Collision2D col) {
		if(col.gameObject.name == "Enemy(Clone)"){
			if (transform.localScale.x < 0.2f){
				Die();
			} else {
				shrink(shrinkRate);
				currentRadius = transform.localScale.x;
				currentRadius = currentRadius/2;
				Debug.Log(currentRadius);
			}
		}
	}
	void Die() {
		Destroy(gameObject);
		GameManagerScript gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		gameManagerScript.RestartGame();
	}
	public void LoadBullet() {
		direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = (Vector2)(direction);
		direction.Normalize();
		direction = direction * loadedBulletRadius;
		bullet = Instantiate(bulletPreFab,direction,Quaternion.identity);
		BulletScript bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.isReady = true;
		isLoaded = true;
	}
	//fire bullet
	void fireBullet() {
		direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = (Vector2)(direction);
		direction.Normalize();
		BulletScript bulletScript = bullet.GetComponent<BulletScript>();
		bulletScript.isReady = false;
		Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
		bulletRB.AddForce(bulletSpeed * direction);

	}
	public void shrink(float rate) {
		float currentSize = transform.localScale.x;
		transform.localScale = new Vector2 (currentSize - rate,currentSize - rate);
		GetComponent<Rigidbody2D>().mass = GetComponent<Rigidbody2D>().mass - rate;
	}
}
