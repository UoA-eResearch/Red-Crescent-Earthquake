using UnityEngine;
using System.Collections;

public class PickupBag : MonoBehaviour {

    public GameObject _closedBag;
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
        if (other.name == "bagClosed")
        {

            pickedUp = true;
            _closedBag.SetActive(false);
          
            Instantiate(ParticleSuccess, ParticleSpawn.transform.position, Quaternion.identity);
            Success.Play();
        }
    }
}
