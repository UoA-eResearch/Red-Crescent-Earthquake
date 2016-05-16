using UnityEngine;
using System.Collections;

public class destroyItselfSlippers : MonoBehaviour {

    public GameObject _destroyer;
    public GameObject ParticleSpawn;
    public GameObject ParticleSuccess;
    public GameObject _left;
    public GameObject _right;
    public GameObject _arrowOfSlippers;
    public AudioSource Success;
    public bool pickedUp;
    // Use this for initialization
    void Start () {
     //  Success = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other)
    {

        if (other.gameObject == _destroyer)
        {
            pickedUp = true;
            _left.SetActive(false);
            _right.SetActive(false);
            _arrowOfSlippers.SetActive(false);
            Instantiate(ParticleSuccess, ParticleSpawn.transform.position, Quaternion.identity);
            Success.Play();
        }

    }
}
