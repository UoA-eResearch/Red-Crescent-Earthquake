using UnityEngine;
using System.Collections;
using System.Collections.Generic;  				// added by Andrew for Lists
using UnityEngine.UI;

public class sequenceManager : MonoBehaviour {

	private Text _tvText;
	private Text _timerText;
	private string _timeString;
	private float _timerStart;
	private float _timeRemaining;
	private Renderer _tvImage;
	private int _itemsTotal = 8;				
	public int _itemsCollected;			    
	private bool _checkItem;
	private string _itemName;
    private EarthquakeController _earthquakeController;
	private Transform _timerRenderer;
	public bool _quakeHasStarted;				//may not need to be pulic
	private GameObject _redCircleUnderTable;
	public GameObject _greenCircleUnderTable;
	private GameObject _holdTarget;
	public GameObject ClosedBag;
	public GameObject OpenBag;
	public bool _headUnderTable;
	public bool _handOnHoldTarget;



	// Audio for the TV
	private AudioSource _tvAudioSource;
	public AudioClip warning;
	public AudioClip intro;
	public AudioClip introPreTime;
	public AudioClip introTime;
	public AudioClip introPostTime;

	public AudioClip rollBandage;
	public AudioClip alcoholWipes;
	public AudioClip bandages;
	public AudioClip firstAidBook;
	public AudioClip ventilator;
	public AudioClip safetyPin;
	public AudioClip scissors;
	public AudioClip triangularBandage;

	public AudioClip getUnderTable;
	public AudioClip holdOn;

	public List<AudioClip> noAudio = new List<AudioClip> ();
	public List<AudioClip> yesAudio = new List<AudioClip> ();
    // end of Audio for TV
    
	// Audio for Hammer Sequence
	public AudioClip hammerIntro;
	public AudioClip target1done;
	public AudioClip target2done;
	public AudioClip topCorner;
	public AudioClip bracket1done;
	public AudioClip bracket2done;

	//Hammer sequence check
	public bool isTarget1done = false;
	public bool isTarget2done = false;

	// Textures for the TV
	// New Order: roll, alc, cpr v, manual, band, tri, pins, scissors
	public Material alcoholWipesImg;
	public Material bandagesImg;
	public Material firstAidBookImg;
	public Material ventilatorImg;
	public Material rollBandageImg;
	public Material safetyPinImg;
	public Material scissorsImg;
	public Material triangularBandageImg;
	public Material lBracket;
	public Material dropCoverHoldImg;
	public Material gasElecSwitchImg;


	// Hammer Targets
	private GameObject _hammerTarget1;
	private GameObject _hammerTarget2;
	private GameObject _hammerTarget3;
	private GameObject _hammerTarget4;


	// for Arrow Sequence
	private GameObject _arrow;
	private Vector3 _rollBandageStartPos;
	private Vector3 _bracketStartPosition;
	public int _arrowSequenceStep = 0;  // return to private after testing.  Andrew.
	private Transform _rollBandage;
	private GameObject _bag;
	private GameObject _bracket;

	void Start () {
		_tvText = GameObject.Find("Dynamic GUI/TV Text").GetComponent<Text>();
		_timerText = GameObject.Find("Dynamic GUI/Timer Text").GetComponent<Text>();
		_tvAudioSource = GameObject.Find("Sequence Manager/TV Audio Source").GetComponent<AudioSource>();
		_tvImage = GameObject.Find("Dynamic GUI/Image").GetComponent<Renderer>();
		_earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();

		// Find and deactivate all hammer targets.  Each will be activated later during the sequence.
		_hammerTarget1 = GameObject.Find("Hammer Target 1");
		_hammerTarget2 = GameObject.Find("Hammer Target 2");
		_hammerTarget3 = GameObject.Find("Hammer Target 3");
		_hammerTarget4 = GameObject.Find("Hammer Target 4");
		_hammerTarget1.SetActive(false);			
		_hammerTarget2.SetActive(false);
		_hammerTarget3.SetActive(false);
		_hammerTarget4.SetActive(false);

		_redCircleUnderTable = GameObject.Find("Red Circle Under Table");
		//_greenCircleUnderTable = GameObject.Find("Green Circle Under Table");
		_holdTarget = GameObject.Find("Hold Target");

		_redCircleUnderTable.SetActive(false);
		_greenCircleUnderTable.SetActive(false);
		_holdTarget.SetActive(false);

		// for Arrow Sequence
		_arrow = GameObject.Find("Arrow");
		_arrow.SetActive(false);
		_rollBandage = GameObject.Find("roll bandage").transform;
		_rollBandageStartPos = _rollBandage.position;
		_bag = GameObject.Find("1st Aid Bag");
		_bracket = GameObject.FindWithTag("bracket");
		_bracketStartPosition = _bracket.transform.position;

		//noAudio = new List<AudioClip> ();

		//_timerRenderer = GameObject.Find("Timer Text").GetComponent<Renderer>();
		_timerRenderer = GameObject.Find("Timer Text").GetComponent<Transform>();
		_timerRenderer.gameObject.SetActive(false);

		// Begin the game sequence
		StartCoroutine(Intro());
	} // end of Start()
	

	void Update () {
		// update time on TV screen
		_timeRemaining = _timerStart + 179 - Time.time;
		if (_timeRemaining < 0 && _quakeHasStarted == false) {
			_quakeHasStarted = true;
			StopAllCoroutines();	
			StartCoroutine(DropCoverHold());
			// hide TV timer
			_timerRenderer.gameObject.SetActive(false);
		}
			
		_timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(_timeRemaining/60), _timeRemaining % 60);
		if (_quakeHasStarted == false ) {
			_timerText.text = _timeString;
		} else {
			_timerText.text = "";
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
            StopAllCoroutines();		
			StartCoroutine(DropCoverHold());
		}

		if (Input.GetKeyDown(KeyCode.H)) {
			StopAllCoroutines();
			StartCoroutine(HammerIntro());
		}
		ArrowSequence();
	} // end of Update()

	void ArrowSequence () {
		if (_arrowSequenceStep == 1 && _arrow.activeSelf == false) {
			_arrow.SetActive(true);
			_arrow.transform.position = _rollBandage.position;
			_arrowSequenceStep = 2;											// wait until player moves roll bandage
		}
		if (_arrowSequenceStep == 2 && _arrow.activeSelf == true) {
			// check if arrow has moved (picked-up by player)
			if (Vector3.Distance(_rollBandage.position, _rollBandageStartPos) > 0.1f) {
				_arrow.transform.position = _bag.transform.position;
				//_arrowSequenceStep = 3;
			}
		}
		if (_arrowSequenceStep == 3 && rollBandage == null) {
			_arrow.SetActive(false);
			_arrowSequenceStep = 4;
		}	
		if (_arrowSequenceStep == 4 ) {
			_arrow.SetActive(true);
			_arrow.transform.position = _bracket.transform.position;
			// check if arrow has moved (picked-up by player)
			if (Vector3.Distance(_bracket.transform.position, _bracketStartPosition) > 0.1f) {
				_arrowSequenceStep = 5;
			}
		}
		if (_arrowSequenceStep == 5) {
				_arrow.transform.position = _hammerTarget1.transform.position;
			}
		if (_arrowSequenceStep == 6) {			// set by NextHammerTarget() when hammer hits 1st green circle
			_arrow.SetActive(false);			// turn off the arrow.  we are done with it.
			_arrowSequenceStep = 7;
		}
	}


	void LateUpdate () {
		if (_checkItem) {
			if (_tvText.text == _itemName) {
				if (GameObject.Find("alcohol wipes")) {
					StartCoroutine(PackAlcoholWipes());
				} else if (GameObject.Find("first aid manual")) {
					StartCoroutine(PackFirstAidBook());
				} else if (GameObject.Find("ventilator")) {
					StartCoroutine(PackVentilator());
				} else if (GameObject.Find("bandages")) {
					StartCoroutine(PackBandages());
				} else if (GameObject.Find("triangular bandage")) {
					StartCoroutine(PackTriangularBandage());
				} else if (GameObject.Find("safety pins")) {
					StartCoroutine(PackSafetyPin());
				} else if (GameObject.Find("scissors")) {
					StartCoroutine(PackScissors());
				}
			}
			_checkItem = false;
			if (_arrow.activeSelf == true) {
				_arrow.SetActive(false);
			}

		}
	} // end of LateUpdate()


	public void NewItemCollected (string _itemNameImported) {
		_itemName = _itemNameImported;
		_itemsCollected ++;

		if (_itemsCollected >= _itemsTotal) {
			// start hammer sequence
			BagClosed();
			StartCoroutine(HammerIntro());
			return;
		}
		_checkItem = true;
	}

	// SWAPPING OPEN WITH CLOSED BAG
	private void BagClosed()
	{
		ClosedBag.SetActive(true);
		OpenBag.SetActive(false);
	}

	IEnumerator Intro () {
		yield return new WaitForSeconds(2); //just a pause at the beginning
		_tvAudioSource.clip = introPreTime;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(introPreTime.length);
		_timerStart = Time.time;
		_tvAudioSource.clip = introTime;
		_tvAudioSource.Play();
		_timerRenderer.gameObject.SetActive(true);
		yield return new WaitForSeconds(introTime.length);
		_tvAudioSource.clip = introPostTime;
		_tvAudioSource.Play();
		yield return new WaitForSeconds(introPostTime.length);
		StartCoroutine(PackRollBandage());
	}
		
	IEnumerator PackAlcoholWipes () {
		_tvText.text = "alcohol wipes";
		_tvImage.material = alcoholWipesImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = alcoholWipes;
		_tvAudioSource.Play();
	}

	IEnumerator PackBandages () {
		_tvText.text = "bandages";
		_tvImage.material = bandagesImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = bandages;
		_tvAudioSource.Play();
	}

	IEnumerator PackFirstAidBook () {
		_tvText.text = "first aid manual";
		_tvImage.material = firstAidBookImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = firstAidBook;
		_tvAudioSource.Play();
	}

	IEnumerator PackVentilator () {
		_tvText.text = "ventilator";
		_tvImage.material = ventilatorImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = ventilator;
		_tvAudioSource.Play();
	}

	IEnumerator PackRollBandage () {
		_tvText.text = "roll bandage";
		_tvImage.material = rollBandageImg;
		_arrowSequenceStep = 1;

		//yield return new WaitForSeconds(1);
		_tvAudioSource.clip = rollBandage;
		_tvAudioSource.Play();
		yield return null;
	}

	IEnumerator PackSafetyPin () {
		_tvText.text = "safety pins";
		_tvImage.material = safetyPinImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = safetyPin;
		_tvAudioSource.Play();
	}

	IEnumerator PackScissors () {
		_tvText.text = "scissors";
		_tvImage.material = scissorsImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = scissors;
		_tvAudioSource.Play();
	}

	IEnumerator PackTriangularBandage () {
		_tvText.text = "triangular bandage";
		_tvImage.material = triangularBandageImg;
		yield return new WaitForSeconds(1);
		_tvAudioSource.clip = triangularBandage;
		_tvAudioSource.Play();
	}

	IEnumerator DropCoverHold () {
		_quakeHasStarted = true;				// hoping this will stop the 2nd earthquake bug
		_tvText.text = "";
		_tvImage.material = dropCoverHoldImg;
		_tvAudioSource.clip = getUnderTable;  
		_tvAudioSource.Play();
		_redCircleUnderTable.SetActive(true);
		_holdTarget.SetActive(true);
		_greenCircleUnderTable.SetActive(false);
		_arrowSequenceStep = 6;										//NB: this could bug out the arrow sequence if you add steps later.
		yield return new WaitForSeconds(getUnderTable.length);
		_tvAudioSource.clip = holdOn;
		_tvAudioSource.Play();
		_earthquakeController.StartQuake();
		yield return new WaitForSeconds(50);
		Debug.Log("quake has ended");
		StartCoroutine (GasElecSwitches());
	}

	IEnumerator GasElecSwitches () {
		_tvImage.material = gasElecSwitchImg;
		yield return null;
	}
		
	public void NextHammerTarget (int nextStep) {
		if (nextStep == 2) {
			// HAMMER TARGET
			_hammerTarget1.SetActive(false);		
			_hammerTarget2.SetActive(true);
			_tvAudioSource.clip = bracket1done;
			_tvAudioSource.Play();
			isTarget1done = true;
			_arrowSequenceStep = 6;
		} else if (nextStep == 3) {
			// BRACKET TARGET
			_hammerTarget2.SetActive(false);
			_hammerTarget3.SetActive(true);
			_tvAudioSource.clip = target1done;
			_tvAudioSource.Play();
		} else if (nextStep == 4) {
			// HAMMER TARGET
			_hammerTarget3.SetActive(false);
			_hammerTarget4.SetActive(true);
			_tvAudioSource.clip = bracket2done;
			_tvAudioSource.Play();
			isTarget2done = true;
		} else if (nextStep == 5) {
			// SECURING FINISHED
			// START QUAKE
			_hammerTarget4.SetActive(false);
			_tvAudioSource.clip = target2done;
			_tvAudioSource.Play();
			new WaitForSeconds(target2done.length);
			StopAllCoroutines();		
			StartCoroutine(DropCoverHold());
			// trigger start of quake?  or don't bother since it'll be timer based anyway?
		} 
	}

	IEnumerator HammerIntro () {
		_arrowSequenceStep = 4;
		_tvImage.material = lBracket;
		_tvText.text = "";
		_tvAudioSource.clip = hammerIntro;
		_tvAudioSource.Play();
		_hammerTarget1.SetActive(true);
		yield return new WaitForSeconds(_tvAudioSource.clip.length);
		_tvAudioSource.clip = topCorner;
		_tvAudioSource.Play();
	}

	public void PlayNoAudio() {
		new WaitForSeconds (0.8f);										// slight delay for the "bad" sound from bag
		int randomClip = Random.Range(0, noAudio.Count);
		_tvAudioSource.clip = noAudio[randomClip];
		_tvAudioSource.Play();
		//Debug.Log("play no audio + " + _tvAudioSource.clip);
	}

	public void PlayYesAudio() {
		new WaitForSeconds (0.8f);										// slight delay for the "bad" sound from bag
		int randomYesClip = Random.Range(0, yesAudio.Count);
		_tvAudioSource.clip = yesAudio[randomYesClip];
		_tvAudioSource.Play();
		Debug.Log("play yes audio + " + _tvAudioSource.clip);
	}
}
