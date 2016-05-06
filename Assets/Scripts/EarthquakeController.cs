using UnityEngine;
using System.Collections;

public class EarthquakeController : MonoBehaviour {

	// special FX during the quake
	private ParticleSystem _ceilingDustPfx;
	private GameObject _lightSparks1;
	private GameObject _lightSparks2;
	private GameObject _ceilingLight1;
	private bool _light1dead;
	private AudioSource _rumbleAudiosource;
	private AudioSource CitySiren;

	// Room Events Objects

	public GameObject Picture;
    public GameObject Picture2;
	public GameObject Plant;
	public GameObject Lamp;
	public GameObject Mirror;
	public GameObject Vase;
	public GameObject Lamp1;
	public GameObject Lamp2;
    public GameObject SmallVase;
    public GameObject SmallVase2;
    public GameObject ShellObj;
    public GameObject Plant2Obj;
    public GameObject TopVase1;
    public GameObject TopVase2;
    public GameObject GoldVase1;
    public GameObject GoldVase2;
    public GameObject CoffeeCup1;
    public GameObject CoffeeCup2;
    public GameObject Sculpture1;
    public GameObject Sculpture2;
    public GameObject PicSet;

	// camera shake effect
	private Transform _cameraToShake;
	public bool _shakeCamera;
	public float _shakeStartTime;
	public float _shakeDuration;

    public bool _earthquakeSequenceFinished = false;

    // Particle Effects (Sami)

    public GameObject DustBits;
	public GameObject DustThick;
	public GameObject BuildingSmoke1;
	public GameObject BuildingSmoke2;
	public GameObject BuildingSmoke3;
	public GameObject BuildingSmoke4;

	void Start () {
		// Find all of the objects we need for Special FX.
		_ceilingDustPfx = GameObject.Find("Ceiling Dust PFX").GetComponent<ParticleSystem>();
		_lightSparks1 = GameObject.Find("Light Sparks 1");
		_lightSparks2 = GameObject.Find("Light Sparks 2");
		_ceilingLight1 = GameObject.Find("Spotlight 1");
		_rumbleAudiosource = GameObject.Find("Rumble AudioSource").GetComponent<AudioSource>();
		CitySiren = GameObject.Find("CitySiren").GetComponent<AudioSource>();

		// Deactivate some objects. (Activating these objects triggers their effect.)
		_lightSparks1.SetActive(false);
		_lightSparks2.SetActive(false);

		// Find the active camera in the scene.
		if (GameObject.Find("[CameraRig]")) {
			_cameraToShake = GameObject.Find("[CameraRig]").transform;
		} else {
			_cameraToShake = GameObject.Find("FPSController/FirstPersonCharacter").transform;
		}
	}
	

	void Update () {
		if (_shakeCamera) 
		{
			ShakeCamera();
			//Debug.Log("CAMERA SHAKING");
		}

       /* if (_earthquakeSequenceFinished == true)
        {
            _checkList.enabled = true;
        }*/

		// TESTER BUTTON
		//if (Input.GetKeyDown(KeyCode.A))
		//{
		//	Lamps();	
		//}
	}


	public void StartQuake () {						// This function is called by sequenceManager.cs at the end of packing sequence, and also by spacebar.

		CitySiren.Play();
		Invoke ("QuakeStarter", 10);				// Delayed Quake for 10 seconds so player can find table & hear siren
	}


	private void ShakeCamera () {
		float _newX = _cameraToShake.position.x + Random.Range(-0.01f, 0.01f);
		_cameraToShake.position = new Vector3 (_newX, _cameraToShake.position.y, _cameraToShake.position.z);

		if (Time.time > _shakeStartTime + _shakeDuration) {
			_shakeCamera = false;
            _earthquakeSequenceFinished = true;
            
        }
	}

	void QuakeStarter()
	{
		StartCoroutine(QuakeSequence());

		// Room Events

		Invoke ("PictureFall", 5);
        Invoke("TopVase1Fall", 4);
        Invoke("TopVase2Fall", 5);
        Invoke("Coffee1Fall", 2);
        Invoke("Coffee2Fall", 3);
        Invoke("Sculpture1Fall", 9);
        Invoke("Sculpture2Fall", 7);
        Invoke("GoldVase1Fall", 14);
        Invoke("GoldVase2Fall", 13);
        Invoke("Picture2Fall", 11);
        Invoke("PicSetFall", 7);
        Invoke ("MirrorFall", 7);
		Invoke ("PlantFall", 8);
		Invoke ("LampFall", 10);
        Invoke("SmallVaseFall", 4);
        Invoke("SmallVase2Fall", 2);
        Invoke("Shell", 6);
        Invoke ("VaseFall", 3);
        Invoke("Plant2Fall", 9);
        Invoke ("Lamps", 1);
	}

	IEnumerator QuakeSequence () {
		_rumbleAudiosource.Play();
		yield return new WaitForSeconds(0.5f);
		_shakeCamera = true;
		_shakeStartTime = Time.time;
		yield return new WaitForSeconds(0.1f);
		_ceilingDustPfx.Play();
		yield return new WaitForSeconds(4.0f);
		StartCoroutine(KillLights1());


		// Particles
		DustBits.SetActive(true);
		DustThick.SetActive(true);
		BuildingSmoke1.SetActive(true);
		BuildingSmoke2.SetActive(true);
		BuildingSmoke3.SetActive(true);
		BuildingSmoke4.SetActive(true);

		// Audio


		// 
	}


	IEnumerator KillLights1 () {
		_light1dead = true;
		_lightSparks1.SetActive(true);
		_ceilingLight1.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		_lightSparks2.SetActive(true);
	}

	// ROOM EVENTS

	void PictureFall()
	{
		Picture.GetComponent<Rigidbody>().useGravity = true;
	}

    void Picture2Fall()
    {
        Picture2.GetComponent<Rigidbody>().useGravity = true;
    }

    void MirrorFall()
	{
		Mirror.GetComponent<Rigidbody>().useGravity = true;
	}

	void PlantFall()
	{
		Plant.GetComponent<PlantAnim>().PlantFall();
	}

	void LampFall()
	{
		Lamp.GetComponent<Rigidbody>().AddForce (new Vector3 (0, 0, 1.5f), ForceMode.Impulse);
	}

    void SmallVaseFall()
    {
        SmallVase.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1.5f), ForceMode.Impulse);
    }

    void SmallVase2Fall()
    {
        SmallVase2.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1f), ForceMode.Impulse);
    }

    void VaseFall()
	{
		//Vase.GetComponent<Rigidbody>().AddForce (new Vector3 (0.1f, 0, 0), ForceMode.Impulse);
	}

    void Shell()
    {
        ShellObj.GetComponent<Rigidbody>().AddForce(new Vector3(-1.5f, 0, 0), ForceMode.Impulse);
    }

    void Plant2Fall()
    {
        Plant2Obj.GetComponent<Rigidbody>().AddForce(new Vector3(-1.5f, 0, 0), ForceMode.Impulse);
    }

    void TopVase1Fall()
    {
        TopVase1.GetComponent<Rigidbody>().AddForce(new Vector3(1f, 0, 0), ForceMode.Impulse);
    }

    void TopVase2Fall()
    {
        TopVase2.GetComponent<Rigidbody>().AddForce(new Vector3(1f, 0, 0), ForceMode.Impulse);
    }

    void GoldVase1Fall()
    {
        GoldVase1.GetComponent<Rigidbody>().AddForce(new Vector3(1.5f, -0.2f, 0), ForceMode.Impulse);
    }

    void GoldVase2Fall()
    {
        GoldVase2.GetComponent<Rigidbody>().AddForce(new Vector3(-1.5f, 0.2f, 0), ForceMode.Impulse);
    }

    void Coffee1Fall()
    {
        CoffeeCup1.GetComponent<Rigidbody>().AddForce(new Vector3(1.5f, 0.2f, 0), ForceMode.Impulse);
    }

    void Coffee2Fall()
    {
        CoffeeCup2.GetComponent<Rigidbody>().AddForce(new Vector3(1.5f, -0.2f, 0), ForceMode.Impulse);
    }

    void Sculpture1Fall()
    {
        Sculpture1.GetComponent<Rigidbody>().AddForce(new Vector3(1.5f, -0.2f, 0), ForceMode.Impulse);
    }

    void Sculpture2Fall()
    {
        Sculpture2.GetComponent<Rigidbody>().AddForce(new Vector3(1.5f, -0.2f, 0), ForceMode.Impulse);
    }

    void PicSetFall()
    {
        PicSet.GetComponent<Rigidbody>().useGravity = true;
    }

    void Lamps()
	{
		Lamp1.GetComponent<LampSwing1>().Lamp1Go();
		Lamp2.GetComponent<LampSwing2>().Lamp2Go();
	}


}
