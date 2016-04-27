using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private AudioSource _tvAudioSource;
    public AudioClip warning;
    public AudioClip intro;
    public AudioClip introPreTime;
    public AudioClip introTime;
    public AudioClip introPostTime;

    //English
    public AudioClip rollBandage;
    public AudioClip alcoholWipes;
    public AudioClip firstAidBook;
    public AudioClip bandages;
    public AudioClip safetyPin;
    public AudioClip scissors;
    public AudioClip triangularBandage;
    public AudioClip vaseIntro;
    public AudioClip getUnderTable;
    public AudioClip holdOn;

    public List<AudioClip> noAudio = new List<AudioClip>();
    public List<AudioClip> yesAudio = new List<AudioClip>();

    // Audio for Hammer Sequence
    public AudioClip hammerIntro;
    public AudioClip target1done;
    public AudioClip target2done;
    public AudioClip topCorner;
    public AudioClip bracket1done;
    public AudioClip bracket2done;

    // For Gas & Exit Sequence
    public AudioClip SwitchesAudio;
    public AudioClip ExitAudio;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
