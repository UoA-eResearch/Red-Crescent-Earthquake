using UnityEngine;
using System.Collections;

public class Slippers : MonoBehaviour {

    private sequenceManager _sequenceManager;
    public bool isCarried = false;

    // Use this for initialization
    void Start () {
        _sequenceManager = GameObject.Find("Sequence Manager").GetComponent<sequenceManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Rigidbody>().useGravity == false)
        {
            isCarried = true;
            Destroy(this);
            
        }

        if (this.GetComponent<Rigidbody>().useGravity == true)
        {
            isCarried = false;
        }
    }
}
