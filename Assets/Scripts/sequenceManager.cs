using UnityEngine;
using System.Collections;
using System.Collections.Generic;  				// added by Andrew for Lists
using UnityEngine.UI;
using NewtonVR;

public class sequenceManager : MonoBehaviour {

	private Text _tvText;
    private Text _tvTextTr;
	private Text _timerText;
    public Text _curtainText;
    public Text _curtainTextTr;
	private string _timeString;
	private float _timerStart;
	private float _timeRemaining;
	private Renderer _tvImage;
	private int _itemsTotal = 7;				// used to be 8 (with ventilator)		
	public int _itemsCollected;			    
	private bool _checkItem;
	private string _itemName;
    private GameObject[] items;
    private EarthquakeController _earthquakeController;
    public GameObject _flashlight;
    public GameObject _slippers;
    private AudioManager _audioManager;
    private LeverController _leverController;
    private DoorSequence _doorSequence;
    private Transform _timerRenderer;
	public bool _quakeHasStarted;				//may not need to be public
	private GameObject _redCircleUnderTable;
	public GameObject _greenCircleUnderTable;	// defined publicly bacause Unity had trouble finding the gameObject?
	private GameObject _holdTarget;
	public GameObject ClosedBag;
	public GameObject OpenBag;
	public bool _headUnderTable;
	public bool _handOnHoldTarget;
    public bool bagPicked = false;
    public bool lightPicked = false;
    public bool slippersPicked = false;
    private GameObject _bagShiled;
    private bool _exitOn = false;

    // Audio for the TV
    private AudioSource _tvAudioSource;
	
	//Hammer sequence check
	public bool isTarget1done = false;
	public bool isTarget2done = false;

    //Language check
    public bool isEnglish = false;
    public bool isTurkish = false;

    public bool vaseStarted = false;

    // Textures for the TV
    // New Order: roll, alc, cpr v, manual, band, tri, pins, scissors
    public Material warningImg;
    public Material warningImgTr;
	public Material alcoholWipesImg;
	public Material bandagesImg;
	public Material firstAidBookImg;
	public Material ventilatorImg;
	public Material rollBandageImg;
	public Material safetyPinImg;
	public Material scissorsImg;
	public Material triangularBandageImg;
	public Material lBracket;
	public Material vaseImg;
	public Material dropCoverHoldImg;
    public Material dropCoverHoldImgTr;
    public Material gasElecSwitchImg;
	public Material ExitImg;

	// Hammer Targets
	public GameObject _hammerTarget1;					// set to public so that bullseye.cs can move hammer sequence forward
	public GameObject _hammerTarget2;
	public GameObject _hammerTarget3;
	public GameObject _hammerTarget4;

	// for Arrow Sequence
	private GameObject _arrow;
	private Vector3 _rollBandageStartPos;
    private Vector3 _torchStartPos;
    private Vector3 _bagStartPos;
    private Vector3 _slippersStartPos;
	private Vector3 _bracketStartPosition;
	public int _arrowSequenceStep = 0;  // return to private after testing.  Andrew.
	private Transform _rollBandage;
	private GameObject _bag;
	private GameObject _bracket;
	private GameObject _vase;
	private Vector3 _vaseStartPosition;
	//private GameObject _vaseDestination;

	void Start () {
        GameObject.Find("SlippersPlaceHolder").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        _torchStartPos = _flashlight.transform.localPosition;
        Debug.Log("Torch start pos: " + _torchStartPos);

		_tvText = GameObject.Find("Dynamic GUI/TV Text").GetComponent<Text>();
        _tvTextTr = GameObject.Find("Dynamic GUI/TV Text Tr").GetComponent<Text>();
        _timerText = GameObject.Find("Dynamic GUI/Timer Text").GetComponent<Text>();
		_tvAudioSource = GameObject.Find("Audio Manager/TV Audio Source").GetComponent<AudioSource>();
		_tvImage = GameObject.Find("Dynamic GUI/Image").GetComponent<Renderer>();
		_earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();
        _leverController = GameObject.Find("Levers").GetComponent<LeverController>();
        _doorSequence = GameObject.Find("Door1").GetComponent<DoorSequence>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
       // _flashlight = GameObject.Find("FlashLight");
        _slippers = GameObject.Find("SlippersPlaceHolder");
		_bagShiled = GameObject.Find("Bag Shield");

        // Find and deactivate all hammer targets.  Each will be activated later during the sequence.
        _hammerTarget1 = GameObject.Find("Hammer Target 1");
		_hammerTarget2 = GameObject.Find("Hammer Target 2");
		_hammerTarget3 = GameObject.Find("Hammer Target 3");
		_hammerTarget4 = GameObject.Find("Hammer Target 4");
		_hammerTarget1.SetActive(false);			
		_hammerTarget2.SetActive(false);
		_hammerTarget3.SetActive(false);
		_hammerTarget4.SetActive(false);

		//  Circle Under Table
		_redCircleUnderTable = GameObject.Find("Red Circle Under Table");
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
		_vase = GameObject.Find("Vase 1");
		_vaseStartPosition = _vase.transform.position;
		Debug.Log("vase = " + _vase);


		_timerRenderer = GameObject.Find("Timer Text").GetComponent<Transform>();
		_timerRenderer.gameObject.SetActive(false);

        //Check the language
        LanguageCheck();

		// Begin the game sequence
		StartCoroutine(Intro());
	} // end of Start()
	

	void Update () {
		// update time on TV screen
		_timeRemaining = _timerStart + 179 - Time.time;
		if (_timeRemaining < 0 && _quakeHasStarted == false) {
			//_quakeHasStarted = true;								// this boolean is now set in DropCoverHold()
			StopAllCoroutines();	
			DropCoverHold();
			// hide TV timer
			_timerRenderer.gameObject.SetActive(false);
		}


			
		_timeString = string.Format("{0:0}:{1:00}", Mathf.Floor(_timeRemaining/60), _timeRemaining % 60);
		if (_quakeHasStarted == false ) {
			_timerText.text = _timeString;
		} else {
			_timerText.text = "";
		}

		// SHORTCUT FOR VASE SEQUENCE
		if (Input.GetKeyDown(KeyCode.V))
		{
			StopAllCoroutines();		
			VaseIntro();
		}
		// SHORTCUT FOR EARTHQUAKE SEQUENCE
		if (Input.GetKeyDown(KeyCode.Space))
		{
            StopAllCoroutines();		
			DropCoverHold();
		}
		// SHORTCUT FOR BRACKET SEQUENCE
		if (Input.GetKeyDown(KeyCode.H)) 
		{
			StopAllCoroutines();
			StartCoroutine(HammerIntro());
		}

        if(Input.GetKeyDown(KeyCode.L))
        {
            StopAllCoroutines();
            _earthquakeController._earthquakeSequenceFinished = true;
            StartLeverSequence();
            GasElecSwitches();

        }
		// SHORTCUT FOR EXIT SEQUENCE
		if (Input.GetKeyDown(KeyCode.X)) 
		{
			StopAllCoroutines();
            _earthquakeController._earthquakeSequenceFinished = true;
            StartCoroutine(ExitTime());
        } 


        ArrowSequence();
        StartLeverSequence();

        if (_leverController.gasOff == true && _leverController.electricityOff == true && _exitOn == false)
        {
            StartCoroutine(ExitTime());
        }

        if(_quakeHasStarted == true)
        {
            AvoidMakingBag();
        }

	} // end of Update()

    public void LanguageCheck()
    {
        if(PlayerPrefs.GetInt("english") == 1)
        {
            isEnglish = true;
        }

        if(PlayerPrefs.GetInt("turkish") == 1)
        {
            isTurkish = true;
        }
    }

	void ArrowSequence () {
		if (_arrowSequenceStep == 1 && _arrow.activeSelf == false) {
			_arrow.SetActive(true);
			_arrow.transform.position = _rollBandage.position;
			_arrowSequenceStep = 2;											// wait until player moves roll bandage
		}
		if (_arrowSequenceStep == 2 && _arrow.activeSelf == true && _rollBandage != null) {
			// check if roll bandage has moved 
			if (Vector3.Distance(_rollBandage.position, _rollBandageStartPos) > 0.1f) {
				// move arrow to bag
				_arrow.transform.position = _bag.transform.position;
				// when any item enters bag, arrow is deactivated in LateUpdate() by _checkItem
			}
		}
		if (_arrowSequenceStep == 3 && _audioManager.rollBandage == null /*|| _audioManager.rollBandageTr == null*/) {
			_arrow.SetActive(false);
			_arrowSequenceStep = 4;
		}	
		if (_arrowSequenceStep == 4 ) {
			// make arrow visible
			_arrow.SetActive(true);
			// place arrow over bracket
			_arrow.transform.position = _bracket.transform.position;
			// if bracket has moved, turn off arrow
			if (Vector3.Distance(_bracket.transform.position, _bracketStartPosition) > 0.1f) {
				_arrow.SetActive(false);
			}
		}
		if (_arrowSequenceStep == 5) {					// never called?
			Debug.Log("arrow sequence step 5 set... but why?");
		}
		if (_arrowSequenceStep == 6) {
			// place Arrow over Vase
			_arrow.SetActive(true);
			_arrow.transform.position = _vase.transform.position;
			// if Vase moves, place Arrow at vase Destination
			if (Vector3.Distance(_vase.transform.position, _vaseStartPosition) > 0.1f) {
				_arrow.transform.position = new Vector3(-12.3f, 1.5f, 2.5f);
				_arrow.transform.Rotate(90f, 0f, 0f);
				_arrowSequenceStep = 7;
			}
		}
		if (_arrowSequenceStep == 7) {
			// don't do anything
			// we are just waiting for User to put vase into destination trigger
		}
		if (_arrowSequenceStep == 8) {
			// disable the arrow
			_arrow.SetActive(false);
			// start the earthquake
			StopAllCoroutines();		
			DropCoverHold();
			_arrowSequenceStep = 9;
		}
		if (_arrowSequenceStep == 9) {
			// do nothing
			// we are finished with the arrow
		}
	}

	public void VaseIntro () {					// called by bullseye.cs
		_tvText.text = "";
		_tvImage.material = vaseImg;
        vaseStarted = true;
        if (isEnglish == true)
        {
            //yield return new WaitForSeconds(1);
			_tvImage.material = vaseImg;
			_tvAudioSource.clip = _audioManager.vaseIntro;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            //yield return new WaitForSeconds(1);
			_tvImage.material = vaseImg;
            _tvAudioSource.clip = _audioManager.vaseIntroTr;
            _tvAudioSource.Play();
           
        }
		_arrowSequenceStep = 6;
	}


	void LateUpdate () {
		if (_checkItem) {
			if (_tvText.text == _itemName) {
				if (GameObject.Find("alcohol wipes")) {
					StartCoroutine(PackAlcoholWipes());
				} else if (GameObject.Find("first aid manual")) {
					StartCoroutine(PackFirstAidBook());
				
				} else if (GameObject.Find("box of plasters")) {
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

    private void StartLeverSequence()
    {
        if(_earthquakeController._earthquakeSequenceFinished == true)
        {
            _leverController.enabled = true;
            //Debug.Log("LEVERL SEQUENCE ACTIVE");
            //GasElecSwitches();

        }
    }

	IEnumerator Intro () {
        
        if (isEnglish == true)
        {
            _tvImage.material = warningImg;
            yield return new WaitForSeconds(2); //just a pause at the beginning
            _tvAudioSource.clip = _audioManager.introPreTime;
            _tvAudioSource.Play();
            yield return new WaitForSeconds(_audioManager.introPreTime.length);
            _timerStart = Time.time;
            _tvAudioSource.clip = _audioManager.introTime;
            _tvAudioSource.Play();
            _timerRenderer.gameObject.SetActive(true);
            yield return new WaitForSeconds(_audioManager.introTime.length);
            _tvAudioSource.clip = _audioManager.introPostTime;
            _tvAudioSource.Play();
            yield return new WaitForSeconds(_audioManager.introPostTime.length);
            //_bagShiled.SetActive(false);
        }

        if(isTurkish == true)
        {
            _tvImage.material = warningImgTr;
            yield return new WaitForSeconds(2); //just a pause at the beginning
            _tvAudioSource.clip = _audioManager.introTr;
            _tvAudioSource.Play();
            yield return new WaitForSeconds(_audioManager.introTr.length);
            _timerStart = Time.time;
            _tvAudioSource.clip = _audioManager.introBagTr;
            _tvAudioSource.Play();
            _timerRenderer.gameObject.SetActive(true);
            yield return new WaitForSeconds(_audioManager.introBagTr.length);
            //_bagShiled.SetActive(false);
        }

		StartCoroutine(PackRollBandage());
	}
		
	IEnumerator PackAlcoholWipes () {
        if (isEnglish == true)
        { 
            _tvImage.material = alcoholWipesImg;
            _tvText.text = "alcohol wipes";
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.alcoholWipes;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvImage.material = alcoholWipesImg;
            _tvText.text = "alcohol wipes";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "alkollu mendil";
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.alcoholWipesTr;
            _tvAudioSource.Play();
        }
    }

	IEnumerator PackBandages () {
        if (isEnglish == true)
        {
            _tvText.text = "box of plasters";
            _tvImage.material = bandagesImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.bandages;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvText.text = "box of plasters";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "bir kutu yara bandi";
            _tvImage.material = bandagesImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.bandagesTr;
            _tvAudioSource.Play();
        }
       
    }

	IEnumerator PackFirstAidBook () {
        if (isEnglish == true)
        {
            _tvText.text = "first aid manual";
            _tvImage.material = firstAidBookImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.firstAidBook;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvText.text = "first aid manual";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "ilk yardim kitabi";
            _tvImage.material = firstAidBookImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.drawersCupboardsTr;
            _tvAudioSource.Play();
            yield return new WaitForSeconds(_audioManager.drawersCupboardsTr.length);
            _tvAudioSource.clip = _audioManager.firstAidBookTr;
            _tvAudioSource.Play();
        }
	}

	IEnumerator PackRollBandage ()
    {
        if (isEnglish == true)
        {
            _tvText.text = "roll bandage";
            _tvImage.material = rollBandageImg;
            _arrowSequenceStep = 1;

            //yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.rollBandage;
            _tvAudioSource.Play();
            yield return null;
        }

        if(isTurkish == true)
        {
            _tvText.text = "roll bandage";
            _tvText.transform.localScale = new Vector3(0,0,0);
            _tvTextTr.text = "sargi bezi";
            _tvImage.material = rollBandageImg;
            _arrowSequenceStep = 1;

            //yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.rollBandageTr;
            _tvAudioSource.Play();
            yield return null;
        }
       
	}

    IEnumerator PackSafetyPin() {
        if (isEnglish == true)
        {
            _tvText.text = "safety pins";
            _tvImage.material = safetyPinImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.safetyPin;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvText.text = "safety pins";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "cengelli igneler";
            _tvImage.material = safetyPinImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.safetyPinTr;
            _tvAudioSource.Play();
        }
        
	}

	IEnumerator PackScissors () {
        if (isEnglish == true)
        {
            _tvText.text = "scissors";
            _tvImage.material = scissorsImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.scissors;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvText.text = "scissors";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "makas";
            _tvImage.material = scissorsImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.scissorsTr;
            _tvAudioSource.Play();
        }
	}

	IEnumerator PackTriangularBandage () {
        if (isEnglish == true)
        {
            _tvText.text = "triangular bandage";
            _tvImage.material = triangularBandageImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.triangularBandage;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvText.text = "triangular bandage";
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvTextTr.text = "ucgen sargi bezi";
            _tvImage.material = triangularBandageImg;
            yield return new WaitForSeconds(1);
            _tvAudioSource.clip = _audioManager.triangularBandageTr;
            _tvAudioSource.Play();
        }
	}

    public void AvoidMakingBag()
    {
        items = GameObject.FindGameObjectsWithTag("FirstAidItem");

        foreach(GameObject bagItems in items)
        {
            bagItems.GetComponent<NVRInteractableItem>().enabled = false;
        }
    }

	public void DropCoverHold () {
		// make all hammer target (rings) invisible when quake starts
		_hammerTarget1.SetActive(false);
		_hammerTarget2.SetActive(false);
		_hammerTarget3.SetActive(false);
		_hammerTarget4.SetActive(false);

		if (_quakeHasStarted == false)			// hoping this will stop the 2nd earthquake bug
		{
            if (isEnglish == true)
            {
                _quakeHasStarted = true;
                _tvText.text = "";
                _tvImage.material = dropCoverHoldImg;
                _tvAudioSource.clip = _audioManager.getUnderTable;
                _tvAudioSource.Play();
                _redCircleUnderTable.SetActive(true);
                _holdTarget.SetActive(true);
                _greenCircleUnderTable.SetActive(false);
                _arrowSequenceStep = 6;                                 //NB: if you add steps to the arrow sequence, then you'll need to change this int
               // yield return new WaitForSeconds(_audioManager.getUnderTable.length);
               // _tvAudioSource.clip = _audioManager.holdOn;
               // _tvAudioSource.Play();
            }

            if(isTurkish == true)
            {
                _quakeHasStarted = true;
                _tvText.text = "";
                _tvImage.material = dropCoverHoldImgTr; // turkish image will be here
                _tvAudioSource.clip = _audioManager.getUnderTableTr;
                _tvAudioSource.Play();
                _redCircleUnderTable.SetActive(true);
                _holdTarget.SetActive(true);
                _greenCircleUnderTable.SetActive(false);
                _arrowSequenceStep = 6;                                 //NB: if you add steps to the arrow sequence, then you'll need to change this int
               // yield return new WaitForSeconds(_audioManager.getUnderTableTr.length);
               // _tvAudioSource.clip = _audioManager.holdOnTr;
               // _tvAudioSource.Play();
            }

			_earthquakeController.StartQuake();

            Invoke("EarthOver", 37);
			

		}
	}

    void EarthOver()
    {
        GasElecSwitches();
        StartCurtainGUI();
        Debug.Log("POST EARTHQUAKE HAS BEEN CALLED");
    }

    public void StartCurtainGUI()
    {
        if(isEnglish == true)
        {
            _curtainText.enabled = true;
        }

        if(isTurkish == true)
        {
            _curtainTextTr.enabled = true;
        }
    }

    IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

	public void GasElecSwitches () {
        if (isEnglish == true)
        {
            _tvImage.material = gasElecSwitchImg;
            // _leverController.enabled = true;
            _tvAudioSource.clip = _audioManager.SwitchesAudio;
            _tvAudioSource.Play();
            _holdTarget.SetActive(false);
            _greenCircleUnderTable.SetActive(false);
            _redCircleUnderTable.SetActive(false);
            Debug.Log("I AM BEING CALLED");
            
        }

        if(isTurkish == true)
        {
            _tvImage.material = gasElecSwitchImg;
            // _leverController.enabled = true;
            _tvAudioSource.clip = _audioManager.SwitchesAudioTr;
            _tvAudioSource.Play();
            _holdTarget.SetActive(false);
            _greenCircleUnderTable.SetActive(false);
            _redCircleUnderTable.SetActive(false);
            
        }
	}


    IEnumerator ExitTime()
	{
        
            Debug.Log("exittimeEnter");
        GameObject.Find("SlippersPlaceHolder").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("SlippersPlaceHolder").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameObject.Find("Arrow flashlight").GetComponentInChildren<MeshRenderer>().enabled = true;

        GameObject.Find("Arrow slippers").GetComponentInChildren<MeshRenderer>().enabled = true;
        GameObject.Find("slippersCollider").GetComponentInChildren<BoxCollider>().enabled = true;
        if (ClosedBag.activeInHierarchy)
        {
            
            GameObject.Find("Arrow bag").GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        //closed bag, flashlight and slippers are pickable now
        _flashlight.GetComponent<NVRInteractableItem>().enabled = true;
            _slippers.GetComponent<NVRInteractableItem>().enabled = true;
            ClosedBag.GetComponent<NVRInteractableItem>().enabled = true;
        GameObject.Find("DestroyerColliderSlippers").GetComponent<BoxCollider>().enabled = true;
        GameObject.Find("SlippersPlaceHolder").GetComponent<destroyItselfSlippers>().enabled = true;
            _tvImage.material = ExitImg;
            _exitOn = true;

            if (isEnglish == true)
            {


                _tvAudioSource.clip = _audioManager.ExitAudio;
                _tvAudioSource.Play();

            }

            if (isTurkish == true)
            {


                _tvAudioSource.clip = _audioManager.ExitAudioTr;
                _tvAudioSource.Play();
            }
        

        yield return null;
    }

	IEnumerator HammerIntro () {
		_arrowSequenceStep = 4;
		_tvImage.material = lBracket;
        
        if (isEnglish == true)
        {
            _tvText.transform.localScale = new Vector3(0, 0, 0);
            _tvAudioSource.clip = _audioManager.hammerIntro;
            _tvAudioSource.Play();
            _hammerTarget1.SetActive(true);
            _hammerTarget3.SetActive(true);
            yield return new WaitForSeconds(_tvAudioSource.clip.length);
            _tvAudioSource.clip = _audioManager.topCorner;
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            _tvTextTr.transform.localScale = new Vector3(0, 0, 0);
            _tvAudioSource.clip = _audioManager.hammerIntroTr;
            _tvAudioSource.Play();
            _hammerTarget1.SetActive(true);
            _hammerTarget3.SetActive(true);
            yield return new WaitForSeconds(_tvAudioSource.clip.length);
            _tvAudioSource.clip = _audioManager.topCornerTr;
            _tvAudioSource.Play();
        }
	}



	public void PlayNoAudio() {
        if (isEnglish == true)
        {
            new WaitForSeconds(0.8f);                                       // slight delay for the "bad" sound from bag
            int randomClip = Random.Range(0, _audioManager.noAudio.Count);
            _tvAudioSource.clip = _audioManager.noAudio[randomClip];
            _tvAudioSource.Play();
        }

        if(isTurkish == true)
        {
            new WaitForSeconds(0.8f);                                       // slight delay for the "bad" sound from bag
            int randomClip = Random.Range(0, _audioManager.noAudioTr.Count);
            _tvAudioSource.clip = _audioManager.noAudioTr[randomClip];
            _tvAudioSource.Play();
        }
	}

	public void PlayYesAudio() {

		// NOTE: this function is never called from bag script; disabled because it's annoying.  Reactivate it there.

		if (_tvAudioSource.isPlaying == false) 								// was causing bug: hammer intro audio not hear
		{
            if (isEnglish == true)
            {
                new WaitForSeconds(0.8f);                                       // slight delay for the "bad" sound from bag
                int randomYesClip = Random.Range(0, _audioManager.yesAudio.Count);
                _tvAudioSource.clip = _audioManager.yesAudio[randomYesClip];
                _tvAudioSource.Play();
                Debug.Log("play yes audio + " + _tvAudioSource.clip);
            }

            if(isTurkish == true)
            {
                new WaitForSeconds(0.8f);                                       // slight delay for the "bad" sound from bag
                int randomYesClip = Random.Range(0, _audioManager.yesAudioTr.Count);
                _tvAudioSource.clip = _audioManager.yesAudioTr[randomYesClip];
                _tvAudioSource.Play();
                Debug.Log("play yes audio + " + _tvAudioSource.clip);
            }
		}
	}
}
