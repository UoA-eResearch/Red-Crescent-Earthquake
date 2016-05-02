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

    void Update()
    {
        if (PlayerPrefs.GetInt("english") == 1)
        {
            GameObject.Find("FirstAidBagStaticTr").SetActive(false);
            GameObject.Find("SecureFurnitureStaticTr").SetActive(false);
            GameObject.Find("HeavyObjectsStaticTr").SetActive(false);
            GameObject.Find("DropCoverStaticTr").SetActive(false);
            GameObject.Find("GasElecStaticTr").SetActive(false);
            GameObject.Find("ExitStaticTr").SetActive(false);

            Debug.Log("english");
        }

        if (PlayerPrefs.GetInt("turkish") == 1)
        {
            GameObject.Find("FirstAidBagStatic").SetActive(false);
            GameObject.Find("SecureFurnitureStatic").SetActive(false);
            GameObject.Find("HeavyObjectsStatic").SetActive(false);
            GameObject.Find("DropCoverStatic").SetActive(false);
            GameObject.Find("GasElecStatic").SetActive(false);
            GameObject.Find("ExitStatic").SetActive(false);

            Debug.Log("turkish");
        }
    }
	
	void CheckImageLanguage ()
    {
	    

        /*if (PlayerPrefs.GetInt("english") == 0)
        {
            GameObject.Find("FirstAidBagStatic").SetActive(false);
            GameObject.Find("SecureFurnitureStatic").SetActive(false);
            GameObject.Find("HeavyObjectsStatic").SetActive(false);
            GameObject.Find("DropCoverStatic").SetActive(false);
            GameObject.Find("GasElecStatic").SetActive(false);
            GameObject.Find("ExitStatic").SetActive(false);
        }*/

        if (PlayerPrefs.GetInt("turkish") == 1)
        {
            GameObject.Find("FirstAidBagStatic").SetActive(false);
            GameObject.Find("SecureFurnitureStatic").SetActive(false);
            GameObject.Find("HeavyObjectsStatic").SetActive(false);
            GameObject.Find("DropCoverStatic").SetActive(false);
            GameObject.Find("GasElecStatic").SetActive(false);
            GameObject.Find("ExitStatic").SetActive(false);
        }

       /* if (PlayerPrefs.GetInt("turkish") == 0)
        {
            GameObject.Find("FirstAidBagStaticTr").SetActive(false);
            GameObject.Find("SecureFurnitureStaticTr").SetActive(false);
            GameObject.Find("HeavyObjectsStaticTr").SetActive(false);
            GameObject.Find("DropCoverStaticTr").SetActive(false);
            GameObject.Find("GasElecStaticTr").SetActive(false);
            GameObject.Find("ExitStaticTr").SetActive(false);
        }*/
    }
}
