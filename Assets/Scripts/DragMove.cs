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


	//Lives
	public int lives = 5;
	public Text livesText;

	// Player name
	public GameObject playerName;
	public string playerEnteredName;

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

	// Pause
	public bool playerPaused = true;
	public bool playerFirstRun = true;

	// Drag
	public static GameObject DraggedInstance;

	Vector3 _startPosition;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;

	// For writing file
	StreamWriter sw;
	private string filePath = Application.persistentDataPath + "/trialsResult_";
	//private string filePath = "trialsResult_";

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		count = 0;
		SetCountText ();
		listOfPossibleTargets.Add ("1_0");

		livesText.text = "Vidas: " + lives;

		//sw = new StreamWriter(filePath + "pepesapo" + ".txt");   //The file is created or Overwritten outside the Assests Folder.

		//System.IO.File.WriteAllText("C:\blahblah_yourfilepath\yourtextfile.txt", "This is text that goes into the text file");


	}

	// Update is called once per frame
	void Update () {
		if (playerFirstRun == true && playerPaused == true && playerName.GetComponent<InputField>().text != "" && Input.GetMouseButtonDown(0))
		{
			playerFirstRun = false;
			gameOverTextBottom.SetActive (false);
			playerEnteredName = playerName.GetComponent<InputField>().text.ToString();

			playerName.SetActive (false);
			setUp.beginStart ();

			sw = new StreamWriter(filePath + playerEnteredName + ".txt");   //The file is created or Overwritten outside the Assests Folder.
			sw.WriteLine("#player: "+playerEnteredName, true);
			sw.WriteLine("#trial: "+currentTrial, true);
			sw.Flush();

		}
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

		if (playerPaused == false) {
			TimerBar timebar = GameObject.Find ("TimerBar").GetComponent<TimerBar> ();
			// Escribir a archivo
			//sw = new StreamWriter(filePath + playerEnteredName + ".txt");   //The file is created or Overwritten outside the Assests Folder.
			sw.WriteLine("currenTarget "+ timebar.currentTarget +"time "+Time.deltaTime + " pos x" + Input.mousePosition.x + " pos y" + Input.mousePosition.y, true);
			sw.Flush();
		}

	}

	public void OnEndDrag (PointerEventData eventData)
	{
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;

		lives = lives - 1;

		if (lives < 0) {
			this.GameOver ();
		} else {
			livesText.text = "Vidas: " + lives;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		//int pos = listOfPossibleTargets.IndexOf(currentTarget);
		//bool check = listOfPossibleTargets.Contains (currentTarget);

		if (listOfPossibleTargets.Contains (coll.gameObject.name) == true)
		{
			//Collision target = GameObject.Find (coll.gameObject.name).GetComponent<Collision> ();
			TimerBar timebar = GameObject.Find ("TimerBar").GetComponent<TimerBar> ();
			TrialTimerBar trialTimerBar = GameObject.Find ("TrialTimerBar").GetComponent<TrialTimerBar>();
			// Audio
			audio.PlayOneShot(audioCollission);
			DestroyObject (coll.gameObject);


			// Explosion
			Transform explosionAnimation = Instantiate(explosion_prefab) as Transform;
			explosionAnimation.transform.position = coll.gameObject.transform.position;
			explosionAnimation.Find("ExpAnimator").Find("Fire").gameObject.GetComponent<Renderer>().sortingLayerName = "Item";

			if (timebar.paused == false) {
				//setUp.manageItems (currentTrial + 1);
				timebar.DestroyFullTrial (timebar.currentTrialNumOfTargets, timebar.currentTrial);

				sw.WriteLine("player got "+ timebar.currentTarget +"time "+Time.deltaTime, true);
				sw.Flush();

				timebar.endingTrial = false;
				//print ("Current Trial antes->" + currentTrial);
				timebar.currentTrial = timebar.currentTrial + 1;
				timebar.currentTargetNumber = 0;
				timebar.currentTarget = timebar.currentTrial.ToString () + "_0";
				timebar.currentTrialNumOfTargets = setUp.trialList [currentTrial - 1].trialTargets.Count;
				timebar.currentTrialLastTarget = timebar.currentTrial.ToString () + "_" + timebar.currentTrialNumOfTargets;
				timebar.fullTime = setUp.trialList [currentTrial - 1].trialTargets [0].time;
				currentTrial = timebar.currentTrial;

				trialTimerBar.trialCanStart = false;
				//trialTimerBar.startingTime = trialTimerBar.fullTime;
				//trialTimerBar.timerActive = false;
				sw.WriteLine("#player: "+playerEnteredName, true);
				sw.WriteLine("#trial: "+currentTrial, true);
				sw.Flush();

			} else {
				timebar.paused = false;
				trialTimerBar.timerActive = true;
				print (timebar.currentTargetNumber);
				print (timebar.currentTarget);
				//timebar.currentTargetNumber = timebar.currentTargetNumber + 1;
				//timebar.currentTarget = timebar.currentTrial.ToString()+"_"+timebar.currentTargetNumber.ToString();
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
		Destroy (GameObject.Find ("TrialTimerBar"));
		gameOverText.SetActive (true);
		gameOverTextBottom.SetActive (true);
	}
}