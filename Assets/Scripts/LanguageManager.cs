using UnityEngine;
using System.Collections;

public class LanguageManager : MonoBehaviour
{
    public GameObject bookENG;
    public GameObject bookTUR;

    public toMainScene toMainSceneScript;


    public void Awake()
    {
        bookENG = GameObject.FindGameObjectWithTag("English");
        bookTUR = GameObject.FindGameObjectWithTag("Turkish");

        PlayerPrefs.SetInt("english", 0);
        PlayerPrefs.SetInt("turkish", 0);
    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		// end of Andrew's addition

		if(toMainSceneScript.bookENG == true)
        {
            PlayerPrefs.SetInt("english", 1);
        }
        if(toMainSceneScript.bookTUR == true)
        {
            PlayerPrefs.SetInt("turkish", 1);
        }
    }
}
