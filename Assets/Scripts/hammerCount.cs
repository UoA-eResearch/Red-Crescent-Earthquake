using UnityEngine;
using System.Collections;

public class hammerCount : MonoBehaviour
{
    public bool _target1;
    public bool _target2;
    public GameObject _bracketstill1;
    public GameObject _bracketstill2;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == _bracketstill1)
        {
            _target1 = true;
        }

        if (other.gameObject == _bracketstill2)
        {
            _target2 = true;
        }
    }

}
