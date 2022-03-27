using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessHM : MonoBehaviour
{
    public ShowHideHandy _showHideUI;
    public Animator _ani;
    private Mouse_Look _look;
    [Header("FOV"), Space]
    public float _handyFOV;
    public float _normalFOV;

    private void Start()
    {
        _ani = GetComponent<Animator>();
        _look = GetComponent<Mouse_Look>();
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
                _look.FOVChange(_handyFOV);

            }
            if (!show)
            {
                _ani.ResetTrigger("HandyHam");
                _ani.SetTrigger("WalkyHam");
                _look.FOVChange(_normalFOV);

            }
        }
    }
}
