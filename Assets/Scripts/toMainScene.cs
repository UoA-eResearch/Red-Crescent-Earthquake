using UnityEngine;
using System.Collections;
using NewtonVR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class toMainScene : MonoBehaviour {

	public string _loadScene;
	public GameObject _earthquakeBookEN;
	public GameObject _earthquakeBookTR;
	private bool _triggered;
	private float _duration;
	private float _elapsedTime;
    private GameObject introText;
    private GameObject introTextTr;
    public bool bookENG;
    public bool bookTUR;


	void Start () {
	
		_duration = this.GetComponent<ScreenFadeOut> ().fadeTime;
        introText = GameObject.Find("Text");
        introTextTr = GameObject.Find("TextTr");
    }
	
	// Update is called once per frame
	void Update () {
	
		// added by Andrew
		if (Input.GetKey(KeyCode.Space)) {
			bookENG = true;
			//_triggered = true;	
			//SceneManager.LoadScene (_loadScene);
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
            StartCoroutine(DisplayText());

            
			//StartCoroutine (StartFade ());
		}
	}

    IEnumerator DisplayText()
    {
        if (bookENG == true)
        {
            introText.GetComponent<Text>().enabled = true;
            _earthquakeBookTR.GetComponent<NVRInteractableItem>().enabled = false;
            yield return new WaitForSeconds(10);
            this.GetComponent<ScreenFadeOut>().enabled = true;
            //GetComponent<SteamVR_LoadLevel>().enabled = true;
            StartCoroutine(StartFade());
        }
        if (bookTUR == true)
        {
            introTextTr.GetComponent<Text>().enabled = true;
            _earthquakeBookEN.GetComponent<NVRInteractableItem>().enabled = false;
            yield return new WaitForSeconds(10);
            this.GetComponent<ScreenFadeOut>().enabled = true;
            //GetComponent<SteamVR_LoadLevel>().enabled = true;
            StartCoroutine(StartFade());
        }
    }

	IEnumerator StartFade(){
	
		yield return new WaitForSeconds (_duration);

		SceneManager.LoadScene (_loadScene);
	}
}
