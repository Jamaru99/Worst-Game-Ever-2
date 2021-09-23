using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Camera c;
	static TextMesh txtScore;

	private static ushort score = 0;
	public static ushort Score { 
		get {return score; }
		set {score = value; }
	}

	void Start(){
		c = Camera.main;
		txtScore = GameObject.Find ("Score").GetComponent<TextMesh> ();
		txtScore.text = "" + score;
	}

	void Update () {
		if (Input.touchCount > 0) {
			Touch t = Input.GetTouch (0);
			transform.position = new Vector3(c.ScreenToWorldPoint (t.position).x, c.ScreenToWorldPoint (t.position).y, 0);
		}
	}

	public static void IncreaseScore(){
		if(!GameManagement.IsPaused){
			score += 1;
			txtScore.text = "" + score;
		}
	}

}
