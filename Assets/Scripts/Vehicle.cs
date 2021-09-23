using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	byte isGoingUp;

	public ushort initPoint;
	public ushort endPoint;

	static float speed;

	void Start () {
		Respawn ();
		speed = 0.13f;
	}

	void Update () {
		if(!GameManagement.IsPaused && Active()) Move();
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Edge") {
			Player.IncreaseScore ();
			Respawn ();
		}
		if (other.gameObject.name == "Player") {
			GameManagement.SwitchAudio();
			GameManagement.GameOver ();
		}
	}

	void Move(){
		if (isGoingUp == 1)
			transform.position = new Vector2 (transform.position.x, transform.position.y + speed);
		else
			transform.position = new Vector2 (transform.position.x, transform.position.y - speed);
	}

	void Respawn(){
		isGoingUp = (byte) Random.Range (0, 2);
		float xAxisPosition = Random.Range (-2.7f, 2.7f);
		if (isGoingUp == 1) {
			transform.position = new Vector2 (xAxisPosition, -6);
			transform.rotation = new Quaternion (0, 0, 0, 0);
		} else {
			transform.position = new Vector2 (xAxisPosition, 6);
			transform.rotation = new Quaternion (0, 0, 180, 0);
		}
		if(Player.Score % 15 == 0) speed += 0.03f;
	}

	bool Active(){
		return Player.Score % 15 >= initPoint && Player.Score % 15 < endPoint;
	}

	
}