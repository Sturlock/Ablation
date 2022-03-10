using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessHM : MonoBehaviour
{
    public GameObject _handyMan;
    public GameObject _wrist;

    

    // Update is called once per frame
    void Update()
    {

        _handyMan.transform.position = _wrist.transform.position;
        _handyMan.transform.rotation = _wrist.transform.rotation;
        
    }
}
