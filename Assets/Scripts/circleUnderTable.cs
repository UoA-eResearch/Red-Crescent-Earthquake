using UnityEngine;
using System.Collections;

/*This script checks if the player goes under the table*/

public class circleUnderTable : MonoBehaviour {

	private GameObject _redCircle;
	private GameObject _greenCircle;
	private sequenceManager _sequenceManager;
    private EarthquakeController _earthquakeController;
    public bool _headUnderTable;
    public float durationOfStay;

	// Particle System
	public GameObject ParticleSystemTable; 


	// Use this for initialization
	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
        _earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();
		_redCircle = GameObject.Find("Red Circle Under Table");
		_greenCircle = GameObject.Find("Green Circle Under Table");
		//_greenCircle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //if (_earthquakeController._shakeCamera == true)
        //{
            //if (_greenCircle.activeInHierarchy == true)
           // {
                //durationOfStay += Time.deltaTime;
                //Debug.Log("Duration of stay: " + (int)durationOfStay);
           // }
           // else
           // {
                //durationOfStay = 0.0f;
           // }
       // }
    }

	void OnTriggerEnter (Collider other) {
		if (other.name == "BullSkull") {
			if (_redCircle.activeInHierarchy) 
			{
				_redCircle.SetActive(false);
				_greenCircle.SetActive(true);
                _sequenceManager._headUnderTable = true;	// is this bool called by another function? nothing in sequenceManager.
                _headUnderTable = true;
				ParticleSystemTable.SetActive(true);		// Particles On
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "BullSkull") {
			if (_greenCircle.activeInHierarchy) 
			{
				//_greenCircle.SetActive(false);
				//_redCircle.SetActive(true);

				ParticleSystemTable.SetActive(false);		// Particles off
			}
		}
	}
}
