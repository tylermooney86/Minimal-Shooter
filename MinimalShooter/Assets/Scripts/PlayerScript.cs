using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public GameObject bulletPreFab;
	public float bulletSpeed = 100;
	public Vector3 direction;
	bool isLoaded = false;
	GameObject bullet;
	public float loadedBulletRadius = 0.7f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(isLoaded){
			direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			direction = (Vector2)(direction);
			direction.Normalize();
			direction = direction * loadedBulletRadius;
			bullet.transform.position = new Vector3(direction.x, direction.y, 0);
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log("Fire");
				fireBullet();
				isLoaded = false;
			}
		} else {
			direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			direction = (Vector2)(direction);
			direction.Normalize();
			direction = direction * loadedBulletRadius;
			bullet = Instantiate(bulletPreFab,direction,Quaternion.identity);
			isLoaded = true;
		}
	}
	//fire bullet
	void fireBullet() {
		direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = (Vector2)(direction);
		direction.Normalize();
		Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
		bulletRB.AddForce(bulletSpeed * direction);

	}
}
