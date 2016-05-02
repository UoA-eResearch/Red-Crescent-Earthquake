
using UnityEngine;
using System.Collections.Generic;

public class CheckList : MonoBehaviour
{
    private firstAidBag _firstAidBag;
    private GameObject _slippers;
	private sequenceManager _sequenceManager;
	private EarthquakeController _earthquakeController;
	private circleUnderTable _circleUnderTable;
    private LeverController _leverController;
    private DoorSequence _doorSequence;
    private GameObject _flashlight;
    public PickupBag _pickupBag;
    public PickupSlippers _pickupSlippers;
    public PickupTorch _pickupTorch;
    private VaseDestination _vaseDest;
    private bullseye _bullseye2;
    private bullseye _bullseye4;
    // private holdTarget _holdTarget;
    public List<string> _CollectedItems;
    public bool allLeversOff = false;
    public bool earthquakeFinished = false;

   


    void Awake()
    {
        _firstAidBag = GameObject.Find("1st Aid Bag").GetComponent<firstAidBag>();
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		_earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();
		_circleUnderTable = GameObject.Find("Circle Under Table").GetComponent<circleUnderTable>();

		//_holdTarget = GameObject.Find("Hold Target").GetComponent<holdTarget>();
        _leverController = GameObject.Find("Levers").GetComponent<LeverController>();
        _doorSequence = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorSequence>();
        _flashlight = GameObject.Find("FlashLight");
        _slippers = GameObject.Find("SlippersPlaceHolder");
        _vaseDest = GameObject.Find("Vase Destination").GetComponent<VaseDestination>();
        _bullseye2 = GameObject.Find("Hammer Target 2").GetComponent<bullseye>();
        _bullseye4 = GameObject.Find("Hammer Target 4").GetComponent<bullseye>();
        PlayerPrefs.SetInt("bag", 0);
        PlayerPrefs.SetInt("furniture", 0);
        PlayerPrefs.SetInt("cover", 0);
        PlayerPrefs.SetInt("levers", 0);
        PlayerPrefs.SetInt("heavyObj", 0);
        PlayerPrefs.SetInt("exit", 0);
    }

    // Use this for initialization
    void Start()
    {
        _CollectedItems = _firstAidBag.CollectedItems;
		//duration = _earthquakeController._shakeDuration;
        Debug.Log(_CollectedItems.Capacity + " objects were successfully collected.");
        Debug.Log("Collected items are: ");
        DisplayCollectedItems();
		
        CheckStartEndTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(_CollectedItems.Capacity == 8)
        {
            PlayerPrefs.SetInt("bag", 1);
        }

        CheckForHammerTargets();
        CheckForEarthquakeTasks();
        CheckLevers();
        HeavyObjectsCheck();
        ExitCheck();
    }

    private void CheckStartEndTime()
    {
        Debug.Log("Start time: " + _earthquakeController._shakeStartTime);
        Debug.Log("Shake duration: " + _earthquakeController._shakeDuration);
        Debug.Log("Duration of stay: " + _circleUnderTable.durationOfStay); 
    }

    private void DisplayCollectedItems()
    {
        for(int index = 0; index < _CollectedItems.Capacity; index++)
        {
            print("\n" + _CollectedItems[index]);
        }
    }

	private void CheckForHammerTargets()
	{
		if(_bullseye2.hammerSequenceDone == true || _bullseye4.hammerSequenceDone == true)
		{
            PlayerPrefs.SetInt("furniture", 1);
        }
	}

    public void CheckLevers()
    {
        if(_leverController.electricityOff == true && _leverController.gasOff == true)
        {
            PlayerPrefs.SetInt("levers", 1);
            allLeversOff = true;
			_sequenceManager.ExitTime();
        }
    }

    public void HeavyObjectsCheck()
    {
        if(_vaseDest.vaseMoved == true)
        {
            PlayerPrefs.SetInt("heavyObj", 1);
        }
    }

    public void ExitCheck()
    {
        if (_pickupBag.pickedUp == true && _pickupSlippers.pickedUp == true && _pickupTorch.pickedUp == true  && _doorSequence.doorOpened == true)
        {
            PlayerPrefs.SetInt("exit", 1);
        }
    }

	private void CheckForEarthquakeTasks()
	{
        if (_earthquakeController._earthquakeSequenceFinished == true && _circleUnderTable.durationOfStay >= _earthquakeController._shakeDuration)
		{

            PlayerPrefs.SetInt("cover", 1);

        }
	}
}
