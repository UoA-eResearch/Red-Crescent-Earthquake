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
            GameObject.Find("bagTick").SetActive(true);
            GameObject.Find("bagCross").SetActive(false);
        }
        else
        {
            GameObject.Find("bagTick").SetActive(false);
            GameObject.Find("bagCross").SetActive(true);
        }

        if (PlayerPrefs.GetInt("furniture") == 1)
        {
            GameObject.Find("furnitureTick").SetActive(true);
            GameObject.Find("furnitureCross").SetActive(false);
        }
        else
        {
            GameObject.Find("furnitureTick").SetActive(false);
            GameObject.Find("furnitureCross").SetActive(true);
        }

        if (PlayerPrefs.GetInt("cover") == 1)
        {
            GameObject.Find("coverTick").SetActive(true);
            GameObject.Find("coverCross").SetActive(false);
        }
        else
        {
            GameObject.Find("coverTick").SetActive(false);
            GameObject.Find("coverCross").SetActive(true);
        }

        if (PlayerPrefs.GetInt("levers") == 1)
        {
            GameObject.Find("leversTick").SetActive(true);
            GameObject.Find("leversCross").SetActive(false);
        }
        else
        {
            GameObject.Find("leversTick").SetActive(false);
            GameObject.Find("leversCross").SetActive(true);
        }

    }
}
