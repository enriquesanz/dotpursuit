using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;

// Player Script

public class DragMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Sound
	new AudioSource audio;
	public AudioClip audioCollission;

	public SetUpMain setUp;

	// Score
	private int count;
	public Text countText;

	// To timerBar
	public int currentTrial;
	public int currentTrialNumOfTargets;
	public string currentTarget;
	public int currentTargetNumber;
	public string lastTarget;

	public List<string> listOfPossibleTargets = new List<string>();

	public int playerEaten = 0;

	// Explosion
	public Transform explosion_prefab;


	// Game over
	public GameObject gameOverText;
	public GameObject gameOverTextBottom;
	public bool gameOver = false;

	// Drag
	public static GameObject DraggedInstance;

	Vector3 _startPosition;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;

	// For writing file
	StreamWriter sw;


	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		count = 0;
		SetCountText ();
		listOfPossibleTargets.Add ("1_0");

		sw = new StreamWriter("Assets/salida.txt");   //The file is created or Overwritten outside the Assests Folder.

	}

	// region Interface Implementations

	public void OnBeginDrag (PointerEventData eventData)
	{
		DraggedInstance = gameObject;
		_startPosition = transform.position;
		_zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);

		_offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(Input.touchCount > 1)
			return;

		transform.position = Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		) + _offsetToMouse;

		sw.WriteLine("Hola caracola "+currentTrial+" "+System.DateTime.Now, true);

		sw.Flush();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;
		print ("MAAAAAAL");
	}

	void OnCollisionEnter2D(Collision2D coll) {

		int pos = listOfPossibleTargets.IndexOf(currentTarget);
		bool check = listOfPossibleTargets.Contains (currentTarget);
		print ("currentTarget->" + coll.gameObject.name);
		print ("Existe->" + check);
		print ("Pos->" + pos);
//		if (coll.gameObject.name == currentTarget)
		if (listOfPossibleTargets.Contains (coll.gameObject.name) == true)
		{
			//Collision target = GameObject.Find (coll.gameObject.name).GetComponent<Collision> ();
			TimerBar timebar = GameObject.Find ("TimerBar").GetComponent<TimerBar> ();
			// Audio
			audio.PlayOneShot(audioCollission);
			DestroyObject (coll.gameObject);


			// Explosion
			Transform explosionAnimation = Instantiate(explosion_prefab) as Transform;
			explosionAnimation.transform.position = coll.gameObject.transform.position;
			explosionAnimation.Find("ExpAnimator").Find("Fire").gameObject.GetComponent<Renderer>().sortingLayerName = "Item";

			if (timebar.paused == false) {
				timebar.DestroyFullTrial (currentTrialNumOfTargets, currentTrial);
				currentTrial = currentTrial + 1;
//				timebar.currentTargetNumber = 0;

				timebar.endingTrial = false;
				//print ("Current Trial antes->" + currentTrial);
				timebar.currentTrial = timebar.currentTrial + 1;
				timebar.currentTargetNumber = 0;
				timebar.currentTarget = timebar.currentTrial.ToString () + "_0";
				timebar.currentTrialNumOfTargets = setUp.trialList [currentTrial - 1].trialTargets.Count;
				timebar.currentTrialLastTarget = timebar.currentTrial.ToString () + "_" + timebar.currentTrialNumOfTargets;
				timebar.fullTime = setUp.trialList [currentTrial - 1].trialTargets [0].time;

			} else {
				timebar.paused = false;
				timebar.currentTargetNumber = timebar.currentTargetNumber + 1;
				timebar.currentTarget = timebar.currentTrial.ToString()+"_"+timebar.currentTargetNumber.ToString();
			}	
				
			// Score
			count = count + 1;
			SetCountText ();

		}

	}

	void SetCountText()
	{
		countText.text = "Puntos: " + count.ToString ();
	}

	public void GameOver(){
		gameOver = true;

		Destroy (GameObject.Find ("TimerBar"));
		gameOverText.SetActive (true);
		gameOverTextBottom.SetActive (true);
	}
}