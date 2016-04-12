using UnityEngine;
using System.Collections;

public class LampSwing1 : MonoBehaviour {

	private Animator anim;


	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void Lamp1Go()
	{
		anim.SetTrigger("Go");
	}
}
