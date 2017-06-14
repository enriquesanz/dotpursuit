using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SetUpMain : MonoBehaviour {
	public GameObject target; // Targets
	public int numOfTargets;
	public DragMove player;

	//private TargetList targetList = new TargetList();
	List<Target> targetList = new List<Target>();

	// Use this for initialization
	void Start () {
		setUpList ();
		numOfTargets = targetList.Count;
		manageItems ();
		
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
		for (int i = 0; i < targetList.Count; i++) {
			//Instantiate (target, GeneratedPosition (), Target);
			GameObject myTargetInstance = Instantiate(target) as GameObject;
			myTargetInstance.transform.position = GeneratedPosition (i);
			myTargetInstance.name = i.ToString();
			if (i != 0) {
				myTargetInstance.GetComponent<Renderer>().enabled = false;
			}
		}

	}

	Vector3 GeneratedPosition(int i)
	{
		float x,y,z;
		x = targetList [i].x;
		y = targetList [i].y;
		z = targetList [i].z;
		return new Vector3(x,y,z);
	}

	void setUpList()
	{
		targetList.Add (new Target() {x = 6.00f, y = 2.75f, z = 0f});
		targetList.Add (new Target() {x = 5.75f, y = -2.12f, z = 0f});
		targetList.Add (new Target() {x = 0.41f, y = -2.63f, z = 0f});
		targetList.Add (new Target() {x = -7.29f, y = 2.85f, z = 0f});
		targetList.Add (new Target() {x = -3.89f, y = 2.69f, z = 0f});


	}


	[System.Serializable]
	public class Target
	{
		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }
	}

	[System.Serializable]
	public class TargetList
	{
		public List<Target> targets { get; set; }
	}
}
