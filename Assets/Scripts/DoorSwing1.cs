using UnityEngine;
using System.Collections;

public class DoorSwing1 : MonoBehaviour {

	private Animator anim;


	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void DoorSwing1Go()
	{
		anim.SetTrigger("Go");
	}
}
