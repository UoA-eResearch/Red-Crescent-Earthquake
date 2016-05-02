using UnityEngine;
using System.Collections;

public class PickupSlippers : MonoBehaviour {

    public GameObject _slippers;
    public bool pickedUp = false;


    public void OnTriggerExit(Collider other)
    {
        if (other.name == "SlippersPlaceHolder")
        {
            pickedUp = true;
            _slippers.SetActive(false);
        }
    }
}
