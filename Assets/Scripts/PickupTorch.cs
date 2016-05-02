using UnityEngine;
using System.Collections;

public class PickupTorch : MonoBehaviour
{
    public GameObject _flashlight;
    public bool pickedUp = false;

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "FlashLight")
        {
            pickedUp = true;
            _flashlight.SetActive(false);
        }
    }
}
