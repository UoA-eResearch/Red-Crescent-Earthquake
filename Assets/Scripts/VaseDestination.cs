using UnityEngine;
using System.Collections;

public class VaseDestination : MonoBehaviour {

	private sequenceManager _sequenceManager;

	// Use this for initialization
	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform.name == "Vase 1") {
			_sequenceManager._arrowSequenceStep = 8;
		}
	}
}
