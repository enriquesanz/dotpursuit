using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrialTimerBar : MonoBehaviour {

	public float fullTime = 3.0f;
	public float startingTime = 3.0f; 
	public Slider Timer;

	public bool timerActive = false;
	public bool trialCanStart = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		TimerBar timebar = GameObject.Find ("TimerBar").GetComponent<TimerBar> ();

		if (timerActive == true) {
			startingTime -= Time.deltaTime * 60;
			Timer.value = startingTime;

			if (startingTime <= 0) {
				trialCanStart = true;
				GameObject.Find (timebar.currentTrial.ToString ()+"_0").GetComponent<Renderer> ().enabled = true;
				startingTime = fullTime;
			}
		
		}
			
	}
}
