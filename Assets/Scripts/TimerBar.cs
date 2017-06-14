using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

	public float fullTime = 150.0f;
	public float startingTime = 150.0f; 
	public Slider Timer;
	public int currentTarget=0;
	private int numOfTargets;

	new AudioSource audio;
	public AudioClip failAudio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		Timer = GetComponent<Slider> ();
		currentTarget = 0;
	}
	
	// Update is called once per frame
	void Update () {
		numOfTargets = GameObject.Find("BackGround").GetComponent<SetUpMain>().numOfTargets;

		//currentTarget = player.GetComponent <currentTarget>();
		DragMove player = GameObject.Find("Player").GetComponent<DragMove>();
		//currentTarget = player.currentTarget;

		startingTime -= Time.deltaTime * 50;
		Timer.value = startingTime;

		if (startingTime <= 0)
		{ 
			if (currentTarget == player.currentTarget) {
				if (player.playerEaten == 0) {
					audio.PlayOneShot (failAudio);
					Destroy (GameObject.Find (currentTarget.ToString ()));
					currentTarget = currentTarget + 1;
					player.currentTarget = currentTarget;
					player.playerEaten = 0;
				} else {
					player.playerEaten = 0;
				}

			} else {
				currentTarget = player.currentTarget;
			}
				
			if (currentTarget >= numOfTargets) {
				player.GameOver ();
			} else {
				GameObject.Find(currentTarget.ToString()).GetComponent<Renderer>().enabled = true;
			}

			startingTime = fullTime;
		}
			
	}
}
