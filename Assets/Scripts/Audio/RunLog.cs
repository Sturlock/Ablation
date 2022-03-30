using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunLog : MonoBehaviour
{
    [SerializeField]
    private string myText;

    [SerializeField]
    private AudioLogTextControl LogControl;

    public void LogText()
    {
       // LogControl.LogText(myText);
    }

}
