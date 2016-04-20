using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToScoreboardScene : MonoBehaviour
{

    public string _loadScene;
    private GameObject _earthquakeController;
    private DoorSequence _doorSequence;
    private bool _triggered;
    private float _duration;
    private float _elapsedTime;

    // Use this for initialization
    void Start()
    {
        _duration = this.GetComponent<ScreenFadeOut>().fadeTime;
        _earthquakeController = GameObject.Find("Earthquake Controller");
        _doorSequence = GameObject.Find("Door1").GetComponent<DoorSequence>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_earthquakeController.GetComponent<EarthquakeController>()._earthquakeSequenceFinished == true && _doorSequence.doorOpened == true)
        {
            this.GetComponent<ScreenFadeOut>().enabled = true;

            StartCoroutine(StartFade());
        }
    }

    IEnumerator StartFade()
    {

        yield return new WaitForSeconds(_duration + 3f);


        SceneManager.LoadScene(_loadScene);

        //	Debug.Log ("bitti");
    }
}
