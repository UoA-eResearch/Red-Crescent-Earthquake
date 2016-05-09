using UnityEngine;
using System.Collections;

public class PickupTorch : MonoBehaviour
{
    public GameObject _flashlight;
    public bool pickedUp = false;
    private AudioSource Success;
    public GameObject ParticleSpawn;
    public GameObject ParticleSuccess;

    public void Start()
    {
        Success = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "FlashLight")
        {
            pickedUp = true;
            _flashlight.SetActive(false);
           
            Instantiate(ParticleSuccess, ParticleSpawn.transform.position, Quaternion.identity);
            Success.Play();
        }
    }
}
