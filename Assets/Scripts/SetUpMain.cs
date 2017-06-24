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


	// List of targets DEPECRATED
	List<Target> targetList = new List<Target>();
	List<Target> targetList2 = new List<Target>();

	// List of trials NEW
	List<Trial> trialList = new List<Trial>();


	// Use this for initialization
	void Start () {
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

		Debug.Log ("Empezando");
	
		for (int t = 0; t < trialList.Count; t++) {

			for (int i = 0; i < trialList[t].trialTargets.Count; i++) {
				//Instantiate (target, GeneratedPosition (), Target);
				GameObject myTargetInstance = Instantiate(target) as GameObject;
				myTargetInstance.transform.position = GeneratedPosition (t,i);
				myTargetInstance.name = trialList [t].nTrial.ToString() + '_'+ i.ToString();
				listOfTargets.Add (myTargetInstance.name);
				if (i != 0) {
					myTargetInstance.GetComponent<Renderer>().enabled = false;
				}
			}

		}

		nTrial = trialList [0].nTrial;
		nTrialNumOfTargets = trialList [0].trialTargets.Count;
		lastTarget = trialList.Last().nTrial.ToString() + "_" + (trialList.Last().trialTargets.Count - 1);// + trialList[trialList.Count].trialTargets[trialList[trialList.Count].trialTargets.Count]

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
		targetList.Add (new Target() {x = 6.00f, y = 2.75f, z = 0f, time = 1});
		targetList.Add (new Target() {x = 5.75f, y = -2.12f, z = 0f, time = 154});
		targetList.Add (new Target() {x = 0.41f, y = -2.63f, z = 0f, time = 784});
		targetList.Add (new Target() {x = -7.29f, y = 2.85f, z = 0f, time = 867});

		targetList2.Add (new Target() {x = 6.00f, y = 2.75f, z = 0f, time = 1});
		targetList2.Add (new Target() {x = 5.75f, y = -2.12f, z = 0f, time = 154});
		targetList2.Add (new Target() {x = 0.41f, y = -2.63f, z = 0f, time = 784});
		targetList2.Add (new Target() {x = -7.29f, y = 2.85f, z = 0f, time = 867});

		trialList.Add (new Trial () { nTrial = 1, trialType = 4, trialTargets = targetList});
		trialList.Add (new Trial () { nTrial = 2, trialType = 1, trialTargets = targetList2});

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
