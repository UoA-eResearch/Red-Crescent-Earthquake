using UnityEngine;
using System.Collections;

public class PickupBag : MonoBehaviour {

    public GameObject _closedBag;
    public bool pickedUp = false;

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "bagClosed")
        {
            pickedUp = true;
            _closedBag.SetActive(false);
        }
    }
}
