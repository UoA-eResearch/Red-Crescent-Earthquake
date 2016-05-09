using UnityEngine;
using System.Collections;

public class PickupSlippers : MonoBehaviour {

    public GameObject _slippers;
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
        if (other.name == "SlippersPlaceHolder")
        {
            pickedUp = true;
            _slippers.SetActive(false);
            
            Instantiate(ParticleSuccess, ParticleSpawn.transform.position, Quaternion.identity);
            Success.Play();
        }
    }
}
