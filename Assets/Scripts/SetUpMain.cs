using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class SetUpMain : MonoBehaviour {
	public GameObject target; // Targets
	public int numOfTargets;
	public DragMove player;
	public TimerBar timerBar;

	public List<string> listOfTargets = new List<string>();
	public int nTrial;
	public int nTrialNumOfTargets;
	public string lastTarget;
	public int lastTrial;


	public List<float> trialTime = new List<float> ();
	float totalTime;

	// List of targets DEPECRATED
	List<Target> targetList = new List<Target>();
	List<Target> targetList2 = new List<Target>();
	List<Target> targetList3 = new List<Target>();

	// List of trials NEW
	public List<Trial> trialList = new List<Trial>();

	private Camera camera;



	// Use this for initialization
	void Start () {
		Screen.SetResolution (1920, 960, true);
		setUpList ();
		numOfTargets = targetList.Count;
		manageItems ();
		player.currentTarget = listOfTargets [0];
		player.currentTrial = nTrial;
		player.currentTrialNumOfTargets = nTrialNumOfTargets;
		player.lastTarget = lastTarget;

		timerBar.currentTarget = listOfTargets [0];
		timerBar.currentTrial = nTrial;
		timerBar.currentTrialNumOfTargets = nTrialNumOfTargets;
		timerBar.lastTarget = lastTarget;
		timerBar.lastTrial = lastTrial;

		// Posicionar
		camera = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update () {


		if (player.gameOver == true && Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	void manageItems()
	{
		Destroy (GameObject.Find ("Target"));

		//Debug.Log ("Empezando");
	
		// Create all targets for all trials
		for (int t = 0; t < trialList.Count; t++) {

			for (int i = 0; i < trialList[t].trialTargets.Count; i++) {
				//Instantiate (target, GeneratedPosition (), Target);
				GameObject myTargetInstance = Instantiate(target) as GameObject;
				myTargetInstance.transform.position = GeneratedPosition (t,i);
				myTargetInstance.name = trialList [t].nTrial.ToString() + '_'+ i.ToString();
				listOfTargets.Add (myTargetInstance.name);
				myTargetInstance.GetComponent<Renderer>().enabled = false;
				totalTime = totalTime + trialList [t].trialTargets [i].time;

				Vector3 screenPos = camera.WorldToScreenPoint(GeneratedPosition (t,i));
				Debug.Log("target is " + screenPos.x + " pixels from the left");

			}
			trialTime.Add (totalTime);
			totalTime = 0;
		}

		nTrial = trialList [0].nTrial;
		nTrialNumOfTargets = trialList [0].trialTargets.Count;
		lastTarget = trialList.Last().nTrial.ToString() + "_" + (trialList.Last().trialTargets.Count - 1);// + trialList[trialList.Count].trialTargets[trialList[trialList.Count].trialTargets.Count]
		lastTrial = trialList.Last().nTrial;

		timerBar.startingTime = trialList [0].trialTargets [0].time;
		timerBar.fullTime = trialList [0].trialTargets [0].time;


	}

	Vector3 GeneratedPosition(int t, int i)
	{
		float x,y,z;
		x = trialList[t].trialTargets[i].x;
		y = trialList[t].trialTargets[i].y;
		z = trialList[t].trialTargets[i].z;
		return new Vector3(x,y,z);
	}

	void setUpList()
	{
		targetList.Add (new Target() {x = 6.00f, y = 2.75f, z = 0f, time = 1.0f});
		targetList.Add (new Target() {x = 5.75f, y = -2.12f, z = 0f, time = 154.0f});
		targetList.Add (new Target() {x = 0.41f, y = -2.63f, z = 0f, time = 784.0f});
		targetList.Add (new Target() {x = -7.29f, y = 2.85f, z = 0f, time = 867.0f});

		targetList2.Add (new Target() {x = 7.00f, y = 2.75f, z = 0f, time = 1.0f});
		targetList2.Add (new Target() {x = 6.75f, y = -2.12f, z = 0f, time = 154.0f});
		targetList2.Add (new Target() {x = 1.41f, y = -2.63f, z = 0f, time = 784.0f});
		targetList2.Add (new Target() {x = -8.29f, y = 2.85f, z = 0f, time = 867.0f});

		targetList3.Add (new Target() {x = 5.00f, y = 3.00f, z = 0f, time = 1.0f});
		targetList3.Add (new Target() {x = 3.75f, y = -4.12f, z = 0f, time = 154.0f});
		targetList3.Add (new Target() {x = 4.41f, y = -3.63f, z = 0f, time = 784.0f});


		trialList.Add (new Trial () { nTrial = 1, trialType = 4, trialTargets = targetList});
		trialList.Add (new Trial () { nTrial = 2, trialType = 1, trialTargets = targetList2});
		trialList.Add (new Trial () { nTrial = 3, trialType = 1, trialTargets = targetList3});
		trialList.Add (new Trial () { nTrial = 4, trialType = 3, trialTargets = targetList3});

//		1,4,1203,441,1,607,419,154,911,155,784,896,705,867
//		2,1,607,415,1,1206,475,231,NaN,NaN,776,NaN,NaN,869
//		3,1,607,465,1,1206,425,203,NaN,NaN,836,NaN,NaN,827
//		4,3,1258,423,1,658,468,194,NaN,NaN,733,NaN,NaN,711

	}

	// Trial
	[System.Serializable]
	public class Trial
	{
		public int nTrial { get; set; }
		public int trialType { get; set; }
		public List<Target> trialTargets { get; set; }

	}

	// Target
	[System.Serializable]
	public class Target
	{
		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }
		public float time { get; set; }
	}

	//TargetList
	[System.Serializable]
	public class TargetList
	{
		public List<Target> targets { get; set; }
	}
}
