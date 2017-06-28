using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

	public float fullTime;// = 150.0f;
	public float startingTime;// = 150.0f; 
	public Slider Timer;

	public int currentTrial;
	public int currentTrialNumOfTargets;
	public string currentTarget;
	public int currentTargetNumber;

	public string lastTarget;
	public int lastTrial;

	private int numOfTargets;
	private bool lastRound = false;

	new AudioSource audio;
	public AudioClip failAudio;

	public SetUpMain setUp;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		Timer = GetComponent<Slider> ();
		//currentTarget = 0;
		fullTime = setUp.trialList[0].trialTargets[0].time;
		startingTime = setUp.trialList[0].trialTargets[0].time;
		Debug.Log ("Starting tim2"+startingTime);
	}
	
	// Update is called once per frame
	void Update () {
		numOfTargets = GameObject.Find("BackGround").GetComponent<SetUpMain>().numOfTargets;

		//currentTarget = player.GetComponent <currentTarget>();
		DragMove player = GameObject.Find("Player").GetComponent<DragMove>();
		//currentTarget = player.currentTarget;

		startingTime -= Time.deltaTime * 600;
		Timer.value = startingTime;

		if (startingTime <= 0)
		{ 
			if (lastRound == true) {
				player.GameOver ();
			}

			// Check if currentTargete is the last of the trial
			if (currentTarget == player.currentTrial.ToString() + "_" + (player.currentTrialNumOfTargets - 1)) {
				print ("Destroy trial");
//				for (int t = 0; t < (player.currentTrialNumOfTargets); t++) {
//					Destroy (GameObject.Find (player.currentTrial.ToString() + "_" + t));
//				}
				DestroyFullTrial(player.currentTrialNumOfTargets, player.currentTrial);
			}


			if (currentTarget == player.currentTarget) {
				if (player.playerEaten == 0) {
					if ((currentTrialNumOfTargets - 1) == currentTargetNumber) {
						currentTrial = currentTrial + 1;
						currentTargetNumber = -1;
					}
					audio.PlayOneShot (failAudio);
					//Destroy (GameObject.Find (currentTarget));
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
				

			if (lastRound == false) {
				print ("Activando->" + currentTarget.ToString ());
				GameObject.Find(currentTarget.ToString()).GetComponent<Renderer>().enabled = true;
				player.listOfPossibleTargets.Add (currentTarget.ToString ());

			}

			if (currentTarget == lastTarget) {
				lastRound = true;
			} 

			fullTime = setUp.trialList [currentTrial - 1].trialTargets [currentTargetNumber].time;

			print ("Tiempo->" + fullTime);

			startingTime = fullTime;
		}
			
	}

	public void DestroyFullTrial(int currentTrialNumOfTargets, int currentTrial)
	{
		//print ("Destroying full" + currentTrialNumOfTargets + " " + currentTrial);
		for (int t = 0; t < (currentTrialNumOfTargets); t++) {
			print ("Quitando" + currentTrial.ToString () + "_" + t);
			Destroy (GameObject.Find (currentTrial.ToString() + "_" + t));
		}

		//print ("Current Trial antes->" + currentTrial);
		currentTrial = currentTrial + 1;

		//print ("Last trial->" + lastTrial);
		if (currentTrial > lastTrial) {
			lastRound = true;
		}
	}

}
