using UnityEngine;
using System.Collections;

public class LampSwing2 : MonoBehaviour {

	private Animator anim;


	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void Lamp2Go()
	{
		anim.SetTrigger("Go");
	}
}

