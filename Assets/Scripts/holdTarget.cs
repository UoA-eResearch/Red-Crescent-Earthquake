using UnityEngine;
using System.Collections;

public class holdTarget : MonoBehaviour {

	private Transform _lookTarget;
	private GameObject _redSphere;
	private GameObject _greenSphere;
	private GameObject _sprite;


	// Use this for initialization
	void Start () {
		_lookTarget = GameObject.Find("BullSkull").transform;
		_redSphere = GameObject.Find("Red Sphere");
		_greenSphere = GameObject.Find("Green Sphere");
		_sprite = GameObject.Find("Hold Here Sprite");
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
				_sprite.SetActive(false);


				//ParticleSystemTable.SetActive(true);		// Particles On
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Hand") {
			Debug.Log("hand left trigger");
			if (_greenSphere.activeInHierarchy) 
			{
				_greenSphere.SetActive(false);
				_redSphere.SetActive(true);
				_sprite.SetActive(true);


				//ParticleSystemTable.SetActive(false);		// Particles off
			}
		}
	}
}
