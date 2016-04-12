using UnityEngine;
using System.Collections;

public class PlantAnim : MonoBehaviour 
{
	private Animator anim;



	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void PlantFall()
	{
		anim.SetTrigger("PlantFall");
	}
}
