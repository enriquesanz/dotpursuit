using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONReader : MonoBehaviour {

	string path;
	string jsonString;

	void Start()
	{
		Debug.Log ("Enter reading JSON");
		path = Application.streamingAssetsPath + "/targets.json";
		//path = "/Users/enriquesanz/Documents/Games/Dot Pursuit/Dot Pursuit/Assets/targets.json";
		jsonString = File.ReadAllText (path);

		TargetList targetList = JsonUtility.FromJson<TargetList> (jsonString);
		Debug.Log ("Readed");
		Debug.Log (jsonString);
		Debug.Log (targetList.targets[0].x);

	}

}

[System.Serializable]
public class Target
{
	public float x { get; set; }
	public float y { get; set; }
	public int z { get; set; }
}

[System.Serializable]
public class TargetList
{
	public List<Target> targets { get; set; }
}