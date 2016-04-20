
using UnityEngine;
using System.Collections.Generic;

public class CheckList : MonoBehaviour
{
    private firstAidBag _firstAidBag;
	private sequenceManager _sequenceManager;
	private EarthquakeController _earthquakeController;
	private circleUnderTable _circleUnderTable;
	private holdTarget _holdTarget;
    public List<string> _CollectedItems;
	private float duration;
    public bool earthquakeFinished = false;


    void Awake()
    {
        _firstAidBag = GameObject.Find("1st Aid Bag").GetComponent<firstAidBag>();
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		_earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();
		_circleUnderTable = GameObject.Find("Circle Under Table").GetComponent<circleUnderTable>();
		_holdTarget = GameObject.Find("Hold Target").GetComponent<holdTarget>();
    }

    // Use this for initialization
    void Start()
    {
        _CollectedItems = _firstAidBag.CollectedItems;
		//duration = _earthquakeController._shakeDuration;
        Debug.Log(_CollectedItems.Capacity + " objects were successfully collected.");
        Debug.Log("Collected items are: ");
        DisplayCollectedItems();
		CheckForHammerTargets();
		CheckForEarthquakeTasks();
        CheckStartEndTime();
    }

    // Update is called once per frame
    void Update()
    {
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
		if(_sequenceManager.isTarget1done == true && _sequenceManager.isTarget2done == true)
		{
			Debug.Log("All furnitures were successfully secured.");
		}
		else
		{
			Debug.Log("Furnitures were not successfully secured.");
		}
	}

	private void CheckForEarthquakeTasks()
	{
        if (_earthquakeController._earthquakeSequenceFinished == true && _circleUnderTable.durationOfStay >= _earthquakeController._shakeDuration)
		{

			Debug.Log("The player successfully stayed under the table");
            
		}
		else
		{
			Debug.Log("The player did not successfully stayed under the table");
		}
        if (_earthquakeController._earthquakeSequenceFinished == true && _holdTarget.durationOfHold >= _earthquakeController._shakeDuration)
		{
			Debug.Log("The player successfully hold on to the table leg");
		}
		else
		{
			Debug.Log("The player did not successfully hold on to the table leg");
		}
	}
}
