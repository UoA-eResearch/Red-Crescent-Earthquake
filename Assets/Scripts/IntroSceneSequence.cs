using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroSceneSequence : MonoBehaviour {

    public AudioSource _audioSource;
    

    public AudioClip introTr;
    public AudioClip introEng;

    public bool startFade = false;
    public bool audioTr;

    private GameObject introText;
    private GameObject introTextTr;



    public toMainScene _toMainScene;

	// Use this for initialization
	void Awake ()
    {
        introText = GameObject.Find("Text");
        introTextTr = GameObject.Find("TextTr");

        
    }
	
	// Update is called once per frame
	void Start ()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        if (_toMainScene.bookENG == true)
        {
            introText.GetComponent<Text>().enabled = true;
            _audioSource.clip = introEng;
            _audioSource.Play();
            yield return new WaitForSeconds(introEng.length);
            startFade = true;
        }

        if (_toMainScene.bookTUR == true)
        {
            introTextTr.GetComponent<Text>().enabled = true;
            _audioSource.clip = introTr;
            _audioSource.Play();
            yield return new WaitForSeconds(introTr.length);
            startFade = true;
        }
    }


}
