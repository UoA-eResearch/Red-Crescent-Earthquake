using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToIntroScene : MonoBehaviour {

    public string _loadScene;
    public GameObject _earthquakeManager;
    private bool _triggered;
    private float _duration;
    private float _elapsedTime;

	// Use this for initialization
	void Start () 
    {
        _duration = this.GetComponent<ScreenFadeOut>().fadeTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
            if (_earthquakeManager.GetComponent<EarthquakeController>()._earthquakeSequenceFinished == true)
            {
                this.GetComponent<ScreenFadeOut>().enabled = true;

                StartCoroutine(StartFade());
            }
	}

    IEnumerator StartFade()
    {

        yield return new WaitForSeconds(_duration);


        SceneManager.LoadScene(_loadScene);

        //	Debug.Log ("bitti");
    }
}
