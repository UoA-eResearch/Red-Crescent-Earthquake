using UnityEngine;
using System.Collections;

public class VaseDestination : MonoBehaviour {

	private sequenceManager _sequenceManager;

	// Use this for initialization
	void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
		Debug.Log("seqMan on VaseDestination = " + _sequenceManager);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.transform.name == "Vase 1") {
			Debug.Log("Vase 1 entered trigger on Vase Destination");
		}
	}
}
