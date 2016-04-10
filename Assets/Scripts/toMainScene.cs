using UnityEngine;
using System.Collections;
using NewtonVR;

public class toMainScene : MonoBehaviour {

	public string _loadScene;
	public GameObject _earthquakeBook;
	private bool _triggered;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (_earthquakeBook.GetComponent<NVRInteractableItem> ().IsAttached) {
		

			_triggered = true;
		
		}

		if (_triggered) {
			GetComponent<ScreenFadeOut>().enabled = true;

			if (!GetComponent<ScreenFadeOut> ().isFading) {
			
				Application.LoadLevel (_loadScene);

			}
		
		}

	}
}
