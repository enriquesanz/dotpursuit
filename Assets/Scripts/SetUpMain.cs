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

		targetList.Add (new Target() {x = -7.50f, y = -7f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 5.50f, y = 0f, z = 0f, time = 198f});
		targetList.Add (new Target() {x = -1f, y = -1.45f, z = 0f, time = 758f});
		targetList.Add (new Target() {x = -7f, y = 6.50f, z = 0f, time = 794f});
		trialList.Add (new Trial () { nTrial = 1, trialType = 2, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = 5.0f, y = -6f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = -1.35f, y = 1f, z = 0f, time = 176f});
		targetList.Add (new Target() {x = -4f, y = -7.50f, z = 0f, time = 863f});
		targetList.Add (new Target() {x = -1f, y = 7.50f, z = 0f, time = 835f});
		trialList.Add (new Trial () { nTrial = 2, trialType = 4, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = 7.50f, y = 1f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = -5.50f, y = -7f, z = 0f, time = 226f});
		trialList.Add (new Trial () { nTrial = 3, trialType = 3, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = -1.25f, y = -7f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 5.50f, y = 0f, z = 0f, time = 225f});
		targetList.Add (new Target() {x = -6f, y = -1.45f, z = 0f, time = 762f});
		targetList.Add (new Target() {x = -2f, y = 1.50f, z = 0f, time = 783f});
		trialList.Add (new Trial () { nTrial = 4, trialType = 2, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = -7.50f, y = -7f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 5.50f, y = 0f, z = 0f, time = 204f});
		targetList.Add (new Target() {x = -1f, y = -9.50f, z = 0f, time = 817f});
		targetList.Add (new Target() {x = -7f, y = 1.50f, z = 0f, time = 810f});
		trialList.Add (new Trial () { nTrial = 5, trialType = 2, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = -1.45f, y = -2f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 7.50f, y = -8f, z = 0f, time = 195f});
		trialList.Add (new Trial () { nTrial = 6, trialType = 1, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = -9.50f, y = -2f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 7.50f, y = -3f, z = 0f, time = 198f});
		trialList.Add (new Trial () { nTrial = 7, trialType = 1, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = -7.50f, y = -2f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = 5.50f, y = -5f, z = 0f, time = 167f});
		targetList.Add (new Target() {x = -1f, y = -1.45f, z = 0f, time = 845f});
		targetList.Add (new Target() {x = -2f, y = 1.50f, z = 0f, time = 880f});
		trialList.Add (new Trial () { nTrial = 8, trialType = 2, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = 0f, y = -6.50f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = -8f, y = 5.50f, z = 0f, time = 180f});
		trialList.Add (new Trial () { nTrial = 9, trialType = 5, trialTargets = targetList});
		targetList = new List<Target>();
		targetList.Add (new Target() {x = 2.50f, y = -4f, z = 0f, time = 1f});
		targetList.Add (new Target() {x = -1.05f, y = -2f, z = 0f, time = 220f});
		trialList.Add (new Trial () { nTrial = 10, trialType = 3, trialTargets = targetList});
		targetList = new List<Target>();



		//trialList.Add (new Trial () { nTrial = 4, trialType = 3, trialTargets = targetList3});

		// Resolution 1920 x 960
		// Mid 960 480
//		1,2,-7.50,-7,1,5.50,0,198,-1,-1.45,758,-7,6.50,794
//		2,4,5.0,-6,1,-1.35,1,176,-4,-7.50,863,-1,7.50,835
//		3,3,7.50,1,1,-5.50,-7,226,NaN,NaN,715,NaN,NaN,714
//		4,2,-1.25,-7,1,5.50,0,225,-6,-1.45,762,-2,1.50,783
//		5,2,-7.50,-7,1,5.50,0,204,-1,-9.50,817,-7,1.50,810
//		6,1,-1.45,-2,1,7.50,-8,195,NaN,NaN,767,NaN,NaN,812
//		7,1,-9.50,-2,1,7.50,-3,198,NaN,NaN,714,NaN,NaN,818
//		8,2,-7.50,-2,1,5.50,-5,167,-1,-1.45,845,-2,1.50,880
//		9,5,0,-6.50,1,-8,5.50,180,NaN,NaN,774,NaN,NaN,772
//		10,3,2.50,-4,1,-1.05,-2,220,NaN,NaN,854,NaN,NaN,787

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
