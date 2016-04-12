using UnityEngine;
using System.Collections;

public class holdTarget : MonoBehaviour {

	private Transform _lookTarget;
	private GameObject _redSphere;
	private GameObject _greenSphere;


	// Use this for initialization
	void Start () {
		_lookTarget = GameObject.Find("BullSkull").transform;
		_redSphere = GameObject.Find("Red Sphere");
		_greenSphere = GameObject.Find("Green Sphere");
		_greenSphere.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		// angle UI to User's face
		transform.LookAt(_lookTarget, Vector3.up);
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Hand") {
			if (_redSphere.activeInHierarchy) 
			{
				_redSphere.SetActive(false);
				_greenSphere.SetActive(true);

				//ParticleSystemTable.SetActive(true);		// Particles On
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.name == "BullSkull") {
			if (_greenSphere.activeInHierarchy) 
			{
				_greenSphere.SetActive(false);
				_redSphere.SetActive(true);

				//ParticleSystemTable.SetActive(false);		// Particles off
			}
		}
	}
}
