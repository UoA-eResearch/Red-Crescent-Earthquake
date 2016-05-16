using UnityEngine;
using System.Collections.Generic;

public class firstAidBag : MonoBehaviour {

	private sequenceManager _sequenceManager;
    private EarthquakeController _earthquakeController;
	public AudioSource _sfx;
	public GameObject ParticleSuccess;
	public GameObject ParticleSpawn;
	public GameObject ParticleFailure;
    
	//Audio
	private AudioSource Success;
	private AudioSource Failure;

    [HideInInspector]public List<string> CollectedItems;

    

    void Start () {
		_sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
        _earthquakeController = GameObject.Find("Earthquake Controller").GetComponent<EarthquakeController>();
		Success = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
		Failure = GameObject.Find("FailureSound").GetComponent<AudioSource>();
        CollectedItems = new List<string>();
	}
	

	void Update ()
    {
       
	}


	void OnCollisionEnter (Collision other) 
	{
		if (other.gameObject.tag == "FirstAidItem" && _earthquakeController._earthquakeSequenceFinished == false ) {
			Destroy (other.gameObject);
			_sequenceManager.NewItemCollected(other.gameObject.name);
            CollectedItems.Add(other.gameObject.name);
            Instantiate (ParticleSuccess,ParticleSpawn.transform.position, Quaternion.identity);
			Success.Play();
			//_sequenceManager.PlayYesAudio();
		}


		// DROPPING WRONG ITEM IN BAG
		if (other.gameObject.tag == "WrongItem")
		{
			Destroy (other.gameObject);
			Instantiate (ParticleFailure,ParticleSpawn.transform.position, Quaternion.identity);
			Failure.Play();
			_sequenceManager.PlayNoAudio();
		}
	}
}
