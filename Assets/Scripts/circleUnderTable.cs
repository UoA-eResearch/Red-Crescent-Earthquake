using UnityEngine;
using System.Collections;

public class circleUnderTable : MonoBehaviour {

	private GameObject _redCircle;
	private GameObject _greenCircle;
	private sequenceManager _sequenceManager;

	// Particle System
	public GameObject ParticleSystemTable; 


	// Use this for initialization
	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		_redCircle = GameObject.Find("Red Circle Under Table");
		_greenCircle = GameObject.Find("Green Circle Under Table");
		_greenCircle.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider other) {
		if (other.name == "BullSkull") {
			if (_redCircle.activeInHierarchy) 
			{
				_redCircle.SetActive(false);
				_greenCircle.SetActive(true);
				_sequenceManager._headUnderTable = true;

				ParticleSystemTable.SetActive(true);		// Particles On
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "BullSkull") {
			if (_greenCircle.activeInHierarchy) 
			{
				_greenCircle.SetActive(false);
				_redCircle.SetActive(true);

				ParticleSystemTable.SetActive(false);		// Particles off
			}
		}
	}
}
