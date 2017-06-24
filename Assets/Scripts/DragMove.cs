using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Player Script

public class DragMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Sound
	new AudioSource audio;
	public AudioClip audioCollission;

	// Score
	private int count;
	public Text countText;

	// To timerBar
	public int currentTrial;
	public int currentTrialNumOfTargets;
	public string currentTarget;
	public int currentTargetNumber;
	public string lastTarget;

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


	// Use this for initialization
	void Start () {
		SetUpMain setUp = GetComponent<SetUpMain> ();
		audio = GetComponent<AudioSource>();
		count = 0;
		SetCountText ();

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

	}

	public void OnEndDrag (PointerEventData eventData)
	{
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;
		print ("MAAAAAAL");
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.name == currentTarget)
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



			if (timebar.currentTarget == currentTarget) {
				if (currentTrialNumOfTargets == currentTargetNumber) {
					currentTrial = currentTrial + 1;
					currentTargetNumber = 0;
				}
				playerEaten = playerEaten + 1;
				currentTargetNumber = currentTargetNumber + 1;
				currentTarget = currentTrial + "-" + currentTargetNumber.ToString ();
				timebar.currentTarget = currentTarget;
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