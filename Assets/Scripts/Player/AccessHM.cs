using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessHM : MonoBehaviour
{
    public bool can;
    public ShowHideHandy _showHideUI;
    public Animator _ani;

    private void Start()
    {
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void AccessHandy(bool show)
    {
        
        if (_showHideUI != null)
        {
            if (show)
            {
                _ani.ResetTrigger("WalkyHam");
                _ani.SetTrigger("HandyHam");
                

            }
            if (!show)
            {
                _ani.ResetTrigger("HandyHam");
                _ani.SetTrigger("WalkyHam");
                

            }
        }
    }
}
