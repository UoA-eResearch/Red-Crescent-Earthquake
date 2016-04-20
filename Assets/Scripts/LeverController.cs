using UnityEngine;
using System.Collections;
using NewtonVR;

public class LeverController : MonoBehaviour {

    public GameObject gasAxle;
    public GameObject electricAxle;

    public bool gasOff = false;
    public bool electricityOff = false;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(gasAxle.gameObject.transform.rotation.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(electricAxle.transform.rotation.x >= -0.04316789)
        {
            electricityOff = true;
        }
        else
        {
            electricityOff = false;
        }

        if (gasAxle.transform.rotation.z <= -0.5)
        {
            gasOff = true;
        }
        else
        {
            electricityOff = false;
        }
    }
}
