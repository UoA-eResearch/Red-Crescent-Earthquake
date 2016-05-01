using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        if (PlayerPrefs.GetInt("bag") == 1)
        {
            GameObject.Find("bagCross").SetActive(false);
        }
        if (PlayerPrefs.GetInt("bag") == 0)
        {
            GameObject.Find("bagTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("furniture") == 1)
        {
            GameObject.Find("furnitureCross").SetActive(false);
        }

        if (PlayerPrefs.GetInt("furniture") == 0)
        {
            GameObject.Find("furnitureTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("heavyObj") == 1)
        {
            GameObject.Find("heavyObjectCross").SetActive(false);
        }

        if (PlayerPrefs.GetInt("heavyObj") == 0)
        {
            GameObject.Find("heavyObjectTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("cover") == 1)
        {
            GameObject.Find("coverCross").SetActive(false);
        }

        if (PlayerPrefs.GetInt("cover") == 0)
        {
            GameObject.Find("coverTick").SetActive(false);
        }

        if (PlayerPrefs.GetInt("levers") == 1)
        {
            GameObject.Find("leversCross").SetActive(false);
        }

        if (PlayerPrefs.GetInt("levers") == 0)
        {
            GameObject.Find("leversTick").SetActive(false);
        }

        if(PlayerPrefs.GetInt("exit") == 1)
        {
            GameObject.Find("exitCross").SetActive(false);
        }

        if (PlayerPrefs.GetInt("exit") == 0)
        {
            GameObject.Find("exitTick").SetActive(false);
        }
    }
	
	
    // Update is called once per frame
	void Update ()
    {
	    

    }
}
