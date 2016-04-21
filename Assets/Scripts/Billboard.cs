using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	
    // Update is called once per frame
	void Update ()
    {
	    if(PlayerPrefs.GetInt("bag") == 1)
        {
            GameObject.Find("bagCross").SetActive(false);
        }
        else
        {
            GameObject.Find("bagTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("furniture") == 1)
        {
            GameObject.Find("furnitureCross").SetActive(false);
        }
        else
        {
            GameObject.Find("furnitureTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("cover") == 1)
        {
            GameObject.Find("coverCross").SetActive(false);
        }
        else
        {
            GameObject.Find("coverTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("levers") == 1)
        {
            GameObject.Find("leversCross").SetActive(false);
        }
        else
        {
            GameObject.Find("leversTick").SetActive(false);
        }

    }
}
