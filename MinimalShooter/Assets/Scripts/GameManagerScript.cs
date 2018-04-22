using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

	public GameObject enemyPreFab;

	int enemyCount = 0;
	int levelCount = 0;
	Vector2[] enemyLocations = { new Vector2(4f,2f), new Vector2(-4,0), new Vector2(0,-4), new Vector2(-3,3) };
	float[] enemySizes = {1,2,1.5f,2.5f,1,1,2,2};
	bool isIntro = true;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel")){
			RestartGame();
		}
		if (isIntro){
			if(Input.GetMouseButtonDown(0)){
				isIntro = false;
			}
			if(Input.GetButtonDown("Cancel")) {
				Application.Quit();
			}
		} else {
			GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
			enemyCount = enemyList.Length;
			if (enemyCount <= 0) {
				levelCount++;
				SpawnEnemies(levelCount);
			}
		}

	}
	void SpawnEnemies(int numEnemies) {
		for (int i=0; i<numEnemies; i++){
			Vector2 location = enemyLocations[Random.Range(0,4)];
			GameObject enemy = Instantiate(enemyPreFab, location, Quaternion.identity);
			EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
			enemyScript.shrink(-enemySizes[Random.Range(0,enemySizes.Length)]);
			enemyCount++;
		}
	}
	public void RestartGame() {
		SceneManager.LoadScene("Game");
	}

}
