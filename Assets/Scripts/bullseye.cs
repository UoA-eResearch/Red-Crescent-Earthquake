using UnityEngine;
using System.Collections;

public class bullseye : MonoBehaviour {

	private Transform ring2;
	public float delay;
	private sequenceManager sequenceManager;
	public int nextStep;
	public string _hammerOrBracket = "bracket" ;
    public bool hammerSequenceDone = false;
	// Particle
	public GameObject ParticleSystemSuccess;


	void Start () 
	{
		ring2 = transform.Find("rings/red ring (1)");
		ring2.gameObject.SetActive(false);
		StartCoroutine(Activate2ndRing());
		sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
	}
	

	void Update () {
	
	}

	IEnumerator Activate2ndRing () {				
		yield return new WaitForSeconds(delay);
		ring2.gameObject.SetActive(true);			//this does not work for some reason.
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.transform.tag == "Hammer" && _hammerOrBracket == "hammer") 
		{
			collision.transform.GetComponent<AudioSource>().Play();
			Instantiate (ParticleSystemSuccess, this.transform.position + new Vector3 (0.1f, 0, 0), Quaternion.identity); // Calling Particle System
                                                                                                                          //sequenceManager.NextHammerTarget(nextStep);  necessary?

            // if one target is done, then this is the 2nd target... they're both done... call the next sequence

            if (GameObject.Find("Hammer").GetComponent<hammerCount>()._target1 && GameObject.Find("Hammer").GetComponent<hammerCount>()._target2)
            {
                hammerSequenceDone = true;
                // begin vase sequence

                sequenceManager.VaseIntro();
            }

           // if (sequenceManager._hammerTarget2.activeSelf == false && sequenceManager._hammerTarget4.activeSelf == false) 
			//{
              //  hammerSequenceDone = true;
				// begin vase sequence

               // sequenceManager.VaseIntro();
			//}

			// disable this green bullseye
			transform.gameObject.SetActive(false);
		}

		if (collision.transform.tag == "bracket" && _hammerOrBracket == "bracket") 
		{
			collision.transform.GetComponent<AudioSource>().Play();
			Instantiate (ParticleSystemSuccess, this.transform.position + new Vector3 (0.1f, 0, 0), Quaternion.identity); // Calling Particle System

			// lock bracket into propper position
			collision.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			collision.transform.rotation = transform.rotation;
			collision.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

			// when bracket hits bullseye, change bullseye to Green
			if (transform.name == "Hammer Target 1") 
			{
				sequenceManager._hammerTarget2.SetActive(true);
			}
			if (transform.name == "Hammer Target 3") 
			{
				sequenceManager._hammerTarget4.SetActive(true);
			}

			// disable this Red Bullseye
			transform.gameObject.SetActive(false);	

		}				
	}
}
