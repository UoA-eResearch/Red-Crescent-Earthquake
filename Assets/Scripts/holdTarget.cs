using UnityEngine;
using System.Collections;

public class holdTarget : MonoBehaviour {

	private Transform _lookTarget;
	public GameObject _redSphere;
	public GameObject _greenSphere;
	public GameObject _mySprite;


	// Use this for initialization
	void Start () {
		_lookTarget = GameObject.Find("BullSkull").transform;
		//_redSphere = GameObject.Find("Hold Target/Red Sphere");
		Debug.Log("redsphere = " + _redSphere);
		//_greenSphere = GameObject.Find("Green Sphere");
		_greenSphere.SetActive(false);
		_mySprite = GameObject.Find("sprite");
		Debug.Log("my sprite = " + _mySprite);
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
				Debug.Log("hand in target");
				_redSphere.SetActive(false);
				_greenSphere.SetActive(true);
				_mySprite.SetActive(false);

				//ParticleSystemTable.SetActive(true);		// Particles On
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Hand") {
			if (_greenSphere.activeInHierarchy) 
			{
				_greenSphere.SetActive(false);
				_redSphere.SetActive(true);
				_mySprite.SetActive(true); 

				//ParticleSystemTable.SetActive(false);		// Particles off
			}
		}
	}
}
