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
            GetComponent<SteamVR_LoadLevel>().enabled = true;
			//StartCoroutine (StartFade ());
		}
	}

	IEnumerator StartFade(){
	
		yield return new WaitForSeconds (_duration);

		SceneManager.LoadScene (_loadScene);
	}
}
