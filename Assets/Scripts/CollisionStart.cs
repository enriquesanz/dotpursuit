using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionStart : MonoBehaviour {

	public GameObject timerBar;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Empezamos");
		//Instantiate (explosion, position);
		Destroy (GameObject.Find ("FirstTarget"));
		GameObject.Find ("1_0").GetComponent<Renderer>().enabled = true;
		timerBar.SetActive(true);

	}
}
