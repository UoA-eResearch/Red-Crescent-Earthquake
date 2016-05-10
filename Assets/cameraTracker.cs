using UnityEngine;
using System.Collections;

public class cameraTracker : MonoBehaviour {

    public GameObject _player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = _player.transform.position;
	}
}
