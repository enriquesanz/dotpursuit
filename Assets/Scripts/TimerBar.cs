using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {

	public TrialTimerBar trialTimerBar;

	public float fullTime;// = 150.0f;
	public float startingTime;// = 150.0f; 
	public Slider Timer;

	public bool paused = false;
	public bool playerEaten = false;

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

	//new AudioSource audio;
	public AudioClip failAudio;

	public SetUpMain setUp;

	// Use this for initialization
	void Start () {
		//audio = GetComponent<AudioSource>();
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
			//print ("Vamos por el " + currentTarget);
			startingTime -= Time.deltaTime * 600;
			Timer.value = startingTime;

			// Esto se ejecuta cuando el contador de la barra de tiempo llega a 0
			if (startingTime <= 0)
			{ 

				if (lastRound == true) {
					player.GameOver ();
				}

				if (endingTrial == true || playerEaten == true) {
					if (trialTimerBar.trialCanStart == true) {
						print ("Destroy trial");
						//DestroyFullTrial (currentTrialNumOfTargets, currentTrial);
						for (int t = 0; t < (currentTrialNumOfTargets); t++) {
							print ("Quitando" + currentTrial.ToString () + "_" + t);
							Destroy (GameObject.Find (currentTrial.ToString () + "_" + t));
						}

						endingTrial = false;
						setUp.manageItems (currentTrial);
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

						trialTimerBar.trialCanStart = false;
						trialTimerBar.startingTime = trialTimerBar.fullTime;
						//trialTimerBar.timerActive = false;
						
					}


				} else {
					if (currentTrial > lastTrial) {
						player.GameOver ();
					}


					if (lastRound == false) {
						//print ("Activando->" + currentTarget.ToString ());
						if (currentTarget.ToString () == "1_0") {
							GameObject.Find (currentTarget.ToString ()).GetComponent<Renderer> ().enabled = true;
						}

						if (currentTargetNumber == 0) {
							if (trialTimerBar.trialCanStart == true) {
								GameObject.Find (currentTarget.ToString ()).GetComponent<Renderer> ().enabled = true;
							}
						} else {
							GameObject.Find (currentTarget.ToString ()).GetComponent<Renderer> ().enabled = true;
						}

						player.listOfPossibleTargets.Add (currentTarget.ToString ());	

						if (currentTarget == currentTrialLastTarget) {
							endingTrial = true;
						}

					}

					if (currentTargetNumber == 0) {
						paused = true;
						trialTimerBar.startingTime = trialTimerBar.fullTime;
						//trialTimerBar.timerActive = false;
						//GameObject.Find (currentTrial.ToString ()+"_0").GetComponent<Renderer> ().enabled = true;

					}

					currentTargetNumber = currentTargetNumber + 1;
					currentTarget = currentTrial.ToString()+"_"+currentTargetNumber.ToString();


					fullTime = setUp.trialList [currentTrial - 1].trialTargets [currentTargetNumber -1].time;

					startingTime = fullTime;
				}
			}
		} //paused

	}

	public void DestroyFullTrial(int currentTrialNumOfTargets, int currentTrial)
	{
		//DestroyFullTrial (currentTrialNumOfTargets, currentTrial);
		for (int t = 0; t < (currentTrialNumOfTargets); t++) {
			print ("Quitando" + currentTrial.ToString () + "_" + t);
			Destroy (GameObject.Find (currentTrial.ToString() + "_" + t));
		}

		endingTrial = false;
		//print ("Current Trial antes->" + currentTrial);
		setUp.manageItems (currentTrial);
		currentTrial = currentTrial + 1;

		currentTargetNumber = 0;
		currentTarget = currentTrial.ToString () + "_0";
		currentTrialNumOfTargets = setUp.trialList [currentTrial - 1].trialTargets.Count;
		currentTrialLastTarget = currentTrial.ToString () + "_" + currentTrialNumOfTargets;
		fullTime = setUp.trialList [currentTrial - 1].trialTargets [0].time;
	}

}
