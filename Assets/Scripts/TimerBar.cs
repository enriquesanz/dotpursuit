using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

	public float fullTime = 150.0f;
	public float startingTime = 150.0f; 
	public Slider Timer;

	public int currentTrial;
	public int currentTrialNumOfTargets;
	public string currentTarget;
	public int currentTargetNumber;

	public string lastTarget;

	private int numOfTargets;
	private bool lastRound = false;

	new AudioSource audio;
	public AudioClip failAudio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		Timer = GetComponent<Slider> ();
		//currentTarget = 0;
		SetUpMain setUp = GetComponent<SetUpMain> ();
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
			if (lastRound == true) {
				player.GameOver ();
			}

			if (currentTarget == player.currentTarget) {
				if (player.playerEaten == 0) {
					if ((currentTrialNumOfTargets - 1) == currentTargetNumber) {
						currentTrial = currentTrial + 1;
						currentTargetNumber = -1;
					}
					audio.PlayOneShot (failAudio);
					Destroy (GameObject.Find (currentTarget));
					//currentTarget = currentTarget + 1;
					currentTargetNumber = currentTargetNumber + 1;
					currentTarget = currentTrial.ToString()+"_"+currentTargetNumber.ToString();
					player.currentTarget = currentTarget;
					player.playerEaten = 0;
				} else {
					player.playerEaten = 0;
				}

			} else {
				currentTarget = player.currentTarget;
			}
				
			if (currentTarget == lastTarget) {
				lastRound = true;
			} 

			if (lastRound == false) {
				GameObject.Find(currentTarget.ToString()).GetComponent<Renderer>().enabled = true;

			}
	
			startingTime = fullTime;
		}
			
	}
}
