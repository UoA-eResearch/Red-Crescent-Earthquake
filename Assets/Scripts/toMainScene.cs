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

    public IntroSceneSequence _introSceneSequence;

	void Start () {
	
		_duration = this.GetComponent<ScreenFadeOut> ().fadeTime;
        
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
            FadingSequence();

            
			//StartCoroutine (StartFade ());
		}
	}

    public void FadingSequence()
    {
        if (bookENG == true)
        {
            _earthquakeBookTR.GetComponent<NVRInteractableItem>().enabled = false;

            _introSceneSequence.enabled = true;

            if (_introSceneSequence.startFade == true)
            {
                this.GetComponent<ScreenFadeOut>().enabled = true;
                StartCoroutine(StartFade());
            }
        }
        if (bookTUR == true)
        {
            
            _earthquakeBookEN.GetComponent<NVRInteractableItem>().enabled = false;
            _introSceneSequence.enabled = true;
            
            if (_introSceneSequence.startFade == true)
            {
                this.GetComponent<ScreenFadeOut>().enabled = true;
                StartCoroutine(StartFade());
            }
        }
    }

	IEnumerator StartFade(){
	
		yield return new WaitForSeconds (_duration);

		SceneManager.LoadScene (_loadScene);
	}
}
