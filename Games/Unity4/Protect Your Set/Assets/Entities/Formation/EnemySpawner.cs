﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 11.1f;
	public float height = 6.4f;
	public float speed = 5;
	public float spawnDelay = 0.5f;

	private bool movingRight = true;
	private float xMax, xMin;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xMax = rightBoundary.x;
		xMin = leftBoundary.x;
		SpawnUntilFull();

	}

	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}else{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		float rightEdgeofFormation = transform.position.x + (0.5f*width);
		float leftEdgeofFormation = transform.position.x - (0.5f*width);
		if(leftEdgeofFormation < xMin){
			movingRight = !movingRight;
		}else if(rightEdgeofFormation > xMax){
			movingRight = false;
		}
		if (AllMemberDead()){
			SpawnUntilFull();
		}
	}

	void SpawnFormation(){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;			
		}
	}

	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();

		if(freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;	
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}


	bool AllMemberDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
