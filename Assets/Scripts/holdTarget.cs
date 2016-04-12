using UnityEngine;
using System.Collections;

public class holdTarget : MonoBehaviour {

	private Transform _lookTarget;

	// Use this for initialization
	void Start () {
		_lookTarget = GameObject.Find("BullSkull").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(_lookTarget, Vector3.up);
	}
}
