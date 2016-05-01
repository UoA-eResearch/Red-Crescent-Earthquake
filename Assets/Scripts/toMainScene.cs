using UnityEngine;
using System.Collections;
using NewtonVR;
using UnityEngine.SceneManagement;

public class toMainScene : MonoBehaviour {

	public string _loadScene;
	public GameObject _earthquakeBookEN;
	public GameObject _earthquakeBookTR;
	private bool _triggered;
	private float _duration;
	private float _elapsedTime;

    public bool bookENG;
    public bool bookTUR;


	void Start () {
	
		_duration = this.GetComponent<ScreenFadeOut> ().fadeTime;

	}
	
	// Update is called once per frame
	void Update () {
	
		// added by Andrew
		if (Input.GetKey(KeyCode.Space)) {
			bookENG = true;
			//_triggered = true;	
			SceneManager.LoadScene (_loadScene);
		}
		// end of Andrew's addition


		if (_earthquakeBookEN.GetComponent<NVRInteractableItem> ().IsAttached) {

            bookENG = true;
			_triggered = true;	
		}

		if (_earthquakeBookTR.GetComponentInChildren<NVRInteractableItem> ().IsAttached) {

            bookTUR = true;
			_triggered = true;

		}

		if (_triggered) {
            //this.GetComponent<ScreenFadeOut> ().enabled = true;
            WaitForSeconds(10);
            GetComponent<SteamVR_LoadLevel>().enabled = true;
			//StartCoroutine (StartFade ());
		}
	}

    IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

	IEnumerator StartFade(){
	
		yield return new WaitForSeconds (_duration);

		SceneManager.LoadScene (_loadScene);
	}
}
