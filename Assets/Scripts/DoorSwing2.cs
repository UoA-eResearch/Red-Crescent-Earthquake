using UnityEngine;
using System.Collections;

public class DoorSwing2 : MonoBehaviour {

	private Animator anim;


	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void DoorSwing2Go()
	{
		anim.SetTrigger("Go");
	}
}
