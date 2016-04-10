using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using NewtonVR;

public class fadeInOut : MonoBehaviour {
	public Canvas _faderCanvas;
	public RawImage _faderImage;
	public Color _faderTargetColor;
	private Color _faderColor;
	private Color _faderDefaultColor;

	public GameObject _earthquakeBook;

	public bool _faderTriggered;
	public string _loadScene;


	void Start () {
	
		_faderDefaultColor = _faderImage.color;
		_faderColor = _faderImage.color;

	}
	
	// Update is called once per frame
	void Update () {
		_faderImage.color = _faderColor;


		if (_earthquakeBook.GetComponent<NVRInteractableItem> ().IsAttached) {
		
			_faderTriggered = true;
		
		}

		if (_faderTriggered) {
		
			_faderColor = Color.Lerp (_faderColor, _faderTargetColor, 0.2f * Time.deltaTime);

		}

		if (_faderColor.a > 97f) {
			Application.LoadLevel (_loadScene);
		
		}

	}
}
