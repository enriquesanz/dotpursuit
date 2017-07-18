using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionStart : MonoBehaviour {

	public GameObject timerBar;
	public GameObject trialTimerBar;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Empezamos");

		Destroy (GameObject.Find ("FirstTarget"));

		timerBar.SetActive(true);
		trialTimerBar.SetActive (true);

	}
}
