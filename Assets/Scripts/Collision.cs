using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {
	

	public bool rotation;
	public int position;
	public bool isEnabled = true;

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		// Rotation
		//if (rotation == true)
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		TimerBar timebar = GameObject.Find ("TimerBar").GetComponent<TimerBar> ();
		//DragMove player = GameObject.Find("Player").GetComponent<DragMove>();

		//timebar.paused = false;

//		if (coll.gameObject.name == "1_1") {
//			timebar.DestroyFullTrial(player.currentTrialNumOfTargets, player.currentTrial);
//		}
		//Debug.Log ("Collision " + coll.gameObject.name);
		//Instantiate (explosion, position);
	
	}




}