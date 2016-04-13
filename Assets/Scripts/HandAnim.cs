using UnityEngine;
using System.Collections;

public class HandAnim : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	public void CloseHand ()
    {
        anim.Play("Take 001", -1, 0f);
	}

    public void OpenHand()
    {
        anim.Play("IdleHand", -1, 0f);
    }
}
