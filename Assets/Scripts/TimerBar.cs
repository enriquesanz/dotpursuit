﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

	public float fullTime;// = 150.0f;
	public float startingTime;// = 150.0f; 
	public Slider Timer;

	public bool paused = false;

	public int currentTrial;
	public int currentTrialNumOfTargets;
	public string currentTarget;
	public int currentTargetNumber;
	public string currentTrialLastTarget;

	public string lastTarget;
	public int lastTrial;

	private int numOfTargets;
	private bool lastRound = false;
	public bool endingTrial = false;

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
	}
	
	// Update is called once per frame
	void Update () {

		DragMove player = GameObject.Find("Player").GetComponent<DragMove>();

		currentTrialLastTarget = currentTrial.ToString () + "_" + (currentTrialNumOfTargets - 1).ToString();

		if (paused == false) {
			startingTime -= Time.deltaTime * 600;
			Timer.value = startingTime;

			// Esto se ejecuta cuando el contador de la barra de tiempo llega a 0
			if (startingTime <= 0)
			{ 

				if (lastRound == true) {
					player.GameOver ();
				}

				if (endingTrial == true) {
					print ("Destroy trial");
					//DestroyFullTrial (currentTrialNumOfTargets, currentTrial);
					for (int t = 0; t < (currentTrialNumOfTargets); t++) {
						print ("Quitando" + currentTrial.ToString () + "_" + t);
						Destroy (GameObject.Find (currentTrial.ToString() + "_" + t));
					}

					endingTrial = false;
					//print ("Current Trial antes->" + currentTrial);
					currentTrial = currentTrial + 1;
					currentTargetNumber = 0;
					currentTarget = currentTrial.ToString () + "_0";
					currentTrialNumOfTargets = setUp.trialList [currentTrial - 1].trialTargets.Count;
					currentTrialLastTarget = currentTrial.ToString () + "_" + currentTrialNumOfTargets;
					fullTime = setUp.trialList [currentTrial - 1].trialTargets [0].time;

					//print ("Last trial->" + lastTrial);

					if (currentTrial > lastTrial) {
						lastRound = true;
					}

				} else {
					if (currentTrial > lastTrial) {
						player.GameOver ();
					}

					// Check if currentTargete is the last of the trial
					if (currentTarget == currentTrialLastTarget) {
						endingTrial = true;
					}

					if (lastRound == false) {
						print ("Activando->" + currentTarget.ToString ());
						GameObject.Find(currentTarget.ToString()).GetComponent<Renderer>().enabled = true;
						player.listOfPossibleTargets.Add (currentTarget.ToString ());

					}

					if (currentTargetNumber == 0) {
						paused = true;
					} else {
						currentTargetNumber = currentTargetNumber + 1;
						currentTarget = currentTrial.ToString()+"_"+currentTargetNumber.ToString();

					}

					if (currentTargetNumber == 0) {
						currentTargetNumber = 1;
					}

					fullTime = setUp.trialList [currentTrial - 1].trialTargets [currentTargetNumber -1].time;

					startingTime = fullTime;
				}
			}
		} //paused

	}

	public void DestroyFullTrial(int currentTrialNumOfTargets, int currentTrial)
	{
		//print ("Destroying full" + currentTrialNumOfTargets + " " + currentTrial);
		for (int t = 0; t < (currentTrialNumOfTargets); t++) {
			print ("Quitando" + currentTrial.ToString () + "_" + t);
			Destroy (GameObject.Find (currentTrial.ToString() + "_" + t));
		}

//		endingTrial = false;
//		//print ("Current Trial antes->" + currentTrial);
//		this.currentTrial = currentTrial + 1;
//		currentTargetNumber = 0;
//		currentTarget = currentTrial.ToString () + "_0";
//		currentTrialNumOfTargets = setUp.trialList [currentTrial - 1].trialTargets.Count;
//		currentTrialLastTarget = currentTrial.ToString () + "_" + currentTrialNumOfTargets;
//		fullTime = setUp.trialList [currentTrial - 1].trialTargets [0].time;
//
//		//print ("Last trial->" + lastTrial);
//		if (currentTrial > lastTrial) {
//			lastRound = true;
//		}
	}

}
