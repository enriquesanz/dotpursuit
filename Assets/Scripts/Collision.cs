using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
	

	public bool rotation;
	public int position;
	public bool isEnabled = true;


	// Update is called once per frame
	void Update () {
		// Rotation
		//if (rotation == true)
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log ("Collision " + coll.gameObject.name);
		//Instantiate (explosion, position);
	
	}




}